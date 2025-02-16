﻿using System.Security.Authentication;
using AutoMapper;
using Honey.BL.Authentication.Options;
using Honey.BL.Authentication.RestoringPassword;
using Honey.BL.Services;
using Honey.DB.Entities;
using Honey.DB.Repository.RestoringCode;
using Honey.DB.Repository.User;
using Honey.Domain.Dto.Authentication;
using Honey.Domain.Dto.Authentication.RestoringPassword.ForgotPassword;
using Honey.Domain.Dto.Authentication.RestoringPassword.RestorePassword;
using Honey.Domain.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace Honey.BL.Authentication.Services
{
    public class RestorePasswordService : IRestorePasswordService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authService;
        private readonly IRestoringCodeRepository _codeRepository;
        private readonly SmtpClientOptions _smtpOptions;

        public RestorePasswordService
        (
            IMapper mapper,
            IUserRepository userRepository,
            IAuthenticationService authService,
            IRestoringCodeRepository codeRepository,
            SmtpClientOptions smtpOptions
        )
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _authService = authService;
            _codeRepository = codeRepository;
            _smtpOptions = smtpOptions;
        }
        
        public async Task<ForgotPasswordResponseDto> ForgotPassword(ForgotPasswordRequestDto email)
        {
            var result = new ForgotPasswordResponseDto();

            var user = _userRepository.GetOne(u => u.Email == email.Email);
            if (user == null)
            {
                //result.ErrorType = ErrorType.NotFound;
                return result;
            }
            
            _smtpOptions.Credentials.TryGetValue("UserName", out var fromAddress);
            _smtpOptions.Credentials.TryGetValue("Password", out var password);

            using var smtp = new SmtpClient();
            try
            {
                Enum.TryParse(_smtpOptions.SslProtocol, out SslProtocols sslProtocols);
                smtp.SslProtocols = sslProtocols;

                await smtp.ConnectAsync(_smtpOptions.Host, _smtpOptions.Port, _smtpOptions.EnableSsl);
                await smtp.AuthenticateAsync(fromAddress, password);
                await smtp.SendAsync(await Message(fromAddress, email.Email));
                await smtp.DisconnectAsync(true);

                result = _mapper.Map<ForgotPasswordResponseDto>(email);
                return result;
            }
            catch (Exception e)
            {
                //result.ErrorType = ErrorType.BadRequest;
                return result;
            }
        }
        
        private async Task<MimeMessage> Message(string fromAddress, string toAddress)
        {
            var code = await CreateCode(toAddress);
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Sixgramm", fromAddress));
            emailMessage.To.Add(new MailboxAddress("", toAddress));
            emailMessage.Subject = "[Sixgramm] Восстановление пароля";
            emailMessage.Body = new TextPart(TextFormat.Text)
            {
                Text = $"Восстановите пароль в течение одного дня. \r\n Код для восстановления пароля : {code}"
            };

            return emailMessage;
        }

        public async Task<RestorePasswordResponseDto> RestorePassword(RestorePasswordRequestDto data)
        {
            var result = new RestorePasswordResponseDto();
            if (data.NewPassword != data.ConfirmPassword)
            {
                //result.ErrorType = ErrorType.BadRequest;
                return result;
            }

            var user = _userRepository.GetOne(u => u.Email == data.Email);
            if (user == null)
            {
                //result.ErrorType = ErrorType.NotFound;
                return result;
            }

            var code = _codeRepository.GetOne(c => 
                c.Code == data.Code && 
                c.Email == data.Email && 
                !c.IsUsed &&
                DateTime.Now.Ticks <= c.Expiration);
            
            if (code == null)
            {
                //result.ErrorType = ErrorType.BadRequest;
                return result;
            }

            var updatePasswordDto = new UserPasswordUpdateDto
            {
                Email = data.Email,
                NewPassword = data.NewPassword
            };
            
            await _authService.UpdatePassword(updatePasswordDto);
            await UpdateCode(data.Email, data.Code);
            
            result = _mapper.Map<RestorePasswordResponseDto>(data);
            return result;
        }

        private async Task UpdateCode(string email, string restoringCode)
        {
            var code = _codeRepository.GetOne(c =>
                c.Code == restoringCode &&
                c.Email == email &&
                !c.IsUsed);
            
            code.IsUsed = true;
            code.DateUpdated = DateTime.Now;
            await _codeRepository.Update(code);
        }

        private async Task<string> CreateCode(string email)
        {
            var random = new Random();
            var code = random.Next(100000, 999999).ToString();

            var restoringCode = new RestoringCodeEntity
            {
                Code = code,
                Expiration = DateTime.Today.AddDays(1).Ticks,
                Email = email,
                DateCreated = DateTime.Now
            };
            
            var result = await _codeRepository.Create(restoringCode);
            return result.Code;
        }
    }
}