using Honey.BL.Authentication.RestoringPassword;
using Honey.Domain.Dto.Authentication.Login;
using Honey.Domain.Dto.Authentication.Register;
using Honey.Domain.Dto.Authentication.RestoringPassword.ForgotPassword;
using Honey.Domain.Dto.Authentication.RestoringPassword.RestorePassword;
using Honey.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Honey.Controllers
{
    /// <summary>
    /// Контроллер аутентификации
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IRestorePasswordService _restorePasswordService;
        
        public AuthController
        (
            IAuthenticationService authenticateService,
            IRestorePasswordService restorePasswordService
        )
        {
            _authService = authenticateService;
            _restorePasswordService = restorePasswordService;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <response code="200">Return user Id and token</response>
        /// <response code="400">If the user already exists or Email is not valid</response>
        [HttpPost("[action]")]
        public async Task<ActionResult<UserRegisterResponseDto>> Register(UserRegisterRequestDto user)
        {
            var result = await _authService.Register(user);

            return Ok(result);
        }


        /// <summary>
        /// Логин
        /// </summary>
        /// <param name="user"></param>
        /// <response code="200">Return user Id and token</response>
        /// <response code="404">If the user doesn't exist</response>
        /// <response code="400">If the password is not right</response>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserLoginResponseDto>> Login(UserLoginRequestDto user)
        {
            var result = await _authService.Login(user);

            return Ok(result);
        }

        /// <summary>
        /// Забыть пароль
        /// </summary>
        /// <param name="email"></param>
        /// <response code="200">Return Email</response>
        /// <response code="404">If the user with Email doesn't exist</response>
        /// <response code="400">If Email is not valid</response>
        [HttpPost("Forgot-Password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ForgotPasswordResponseDto>> ForgotPassword(ForgotPasswordRequestDto email)
        {
           var result = await _restorePasswordService.ForgotPassword(email);

           return Ok(result);
        }

        /// <summary>
        /// Восстановить пароль
        /// </summary>
        /// <param name="data"></param>
        /// <response code="200">Return Email</response>
        /// <response code="404">If the user with Email doesn't exist</response>
        /// <response code="400">If new password and confirm password are different or code is not valid</response>
        [HttpPost("Restore-Password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RestorePasswordResponseDto>> RestorePassword(RestorePasswordRequestDto data)
        {
            var result = await _restorePasswordService.RestorePassword(data);

            return Ok(result);
        }
    }
}