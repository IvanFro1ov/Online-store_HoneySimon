using AutoMapper;
using Honey.BL.Authentication.Hashing;
using Honey.BL.Services;
using Honey.Common.Enums.Roles;
using Honey.DB.Entities;
using Honey.DB.Repository.User;
using Honey.Domain.Dto.Authentication;
using Honey.Domain.Dto.Authentication.Login;
using Honey.Domain.Dto.Authentication.Register;
using Honey.Domain.Dto.Token;
using Honey.Domain.Dto.User;
using Honey.Domain.Interfaces;
using Honey.Domain.Validating;

namespace Honey.BL.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService
        (
            ITokenService tokenService,
            IMapper mapper,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserLoginResponseDto> Login(UserLoginRequestDto data)
        {
            var result = new UserLoginResponseDto();

            var user = _userRepository.GetOne(u => u.Email == data.EmailOrUserName || u.UserName == data.EmailOrUserName);
            if (user == null)
            {
                //result.ErrorType = ErrorType.NotFound;
                return result;
            }

            var isPasswordValid = _passwordHasher.PasswordMatches(data.Password, user.Password);
            if (!isPasswordValid)
            {
                //result.ErrorType = ErrorType.BadRequest;
                return result;
            }

            var userDto = _mapper.Map<UserModelDto>(user);

            result = _mapper.Map<UserLoginResponseDto>(user);
            result.Token = CreateToken(userDto);
            return result;
        }

        public async Task<UserRegisterResponseDto> Register(UserRegisterRequestDto data)
        {
            var result = new UserRegisterResponseDto();
            var isEmailValid = EmailValidator.IsEmailValid(data.Email);
            if (!isEmailValid || data.Firstname == null || data.Name == null || data.Age is <= 0 or > 120)
            {
                //result.ErrorType = ErrorType.BadRequest;
                return result;
            }

            var user = _userRepository.GetOne(u => u.Email == data.Email || u.UserName == data.UserName);
            if (user != null)
            {
                //result.ErrorType = ErrorType.BadRequest;
                return result;
            }
            
            var userDto = _mapper.Map<UserModelDto>(data);
            user = _mapper.Map<UserEntity>(data);
            user.Password = _passwordHasher.HashPassword(data.Password);
            user.DateCreated = DateTime.UtcNow;
            user.Role = UserRoles.User;

            result = _mapper.Map<UserRegisterResponseDto>(await _userRepository.Create(user));
            result.Token = CreateToken(userDto);
            return result;
        }
        
        private TokenModelDto CreateToken(UserModelDto data)
        {
            var result = _mapper.Map<TokenModelDto>(_tokenService.CreateToken(data));
            return result;
        }

        public async Task UpdatePassword(UserPasswordUpdateDto data)
        {
            var user = _userRepository.GetOne(u => u.Email == data.Email);
            user.Password = _passwordHasher.HashPassword(data.NewPassword);
            user.DateUpdated = DateTime.Now;
            await _userRepository.Update(user);
        }
    }
}