using Honey.Domain.Dto.Token;

namespace Honey.Domain.Dto.Authentication.Login
{
    public class UserLoginResponseDto
    {
        public Guid Id { get; set; }
        public TokenModelDto Token { get; set; }
    }
}