using Honey.Domain.Dto.User;
using Sixgram.Auth.Core.Token;

namespace Honey.Domain.Interfaces
{
    public interface ITokenService
    {
        TokenModel CreateToken(UserModelDto user);

        /*Guid GetCurrentUserId();

        string GetClaim(string token, string claimType);*/
    }
}