using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Honey.BL.Authentication.Options;
using Honey.Common.Enums.Roles;
using Honey.Domain.Dto.User;
using Honey.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Sixgram.Auth.Core.Token;

namespace Honey.BL.Authentication.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;
        //private readonly HttpContext _httpContext;

        public TokenService(
            AppOptions appOptions 
            /*IHttpContextAccessor httpContext*/)
        {
            //_httpContext = httpContext.HttpContext;
            _secretKey = appOptions.SecretKey;
        }

        public TokenModel CreateToken(UserModelDto user)
        {
            var token= BuildTokenModel(user);
            return token;
        }
        
        /*public Guid GetCurrentUserId() =>
            Guid.TryParse(_httpContext.User.FindFirst(p => p.Type == ClaimTypes.NameIdentifier).Value, out var userId)
                ? userId
                : new Guid();

        public string GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }*/

        private TokenModel BuildTokenModel(UserModelDto user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenExpiration = DateTime.Now.AddDays(365);

            var securityToken = new JwtSecurityToken
            (
                claims : GetClaims(user),
                expires : tokenExpiration,
                notBefore : DateTime.Now,
                signingCredentials : 
                new SigningCredentials (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            
            var token = handler.WriteToken(securityToken);

            return new TokenModel(token, tokenExpiration.Ticks);
        }

        private static IEnumerable<Claim> GetClaims(UserModelDto user)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, user.Id.ToString()),
                new (ClaimTypes.Email, user.Email),
                new (ClaimTypes.Role, UserRoles.User.ToString())
            };

            return claims;
        }
    }
}