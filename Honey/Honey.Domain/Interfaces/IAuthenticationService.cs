using Honey.Domain.Dto.Authentication;
using Honey.Domain.Dto.Authentication.Login;
using Honey.Domain.Dto.Authentication.Register;

namespace Honey.Domain.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthenticationService
    {
        Task<UserLoginResponseDto> Login(UserLoginRequestDto data);
        Task<UserRegisterResponseDto> Register(UserRegisterRequestDto data);
        Task UpdatePassword(UserPasswordUpdateDto data);
    }
}