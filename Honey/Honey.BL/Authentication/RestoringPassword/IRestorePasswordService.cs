using Honey.Domain.Dto.Authentication.RestoringPassword.ForgotPassword;
using Honey.Domain.Dto.Authentication.RestoringPassword.RestorePassword;

namespace Honey.BL.Authentication.RestoringPassword
{
    public interface IRestorePasswordService
    {
        Task<ForgotPasswordResponseDto> ForgotPassword(ForgotPasswordRequestDto email);
        Task<RestorePasswordResponseDto> RestorePassword(RestorePasswordRequestDto data);
    }
}