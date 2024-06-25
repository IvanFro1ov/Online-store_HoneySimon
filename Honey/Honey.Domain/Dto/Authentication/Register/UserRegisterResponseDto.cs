using Honey.Domain.Dto.Token;

namespace Honey.Domain.Dto.Authentication.Register
{
    public class UserRegisterResponseDto
    {
        public Guid Id { get; set; }
        public TokenModelDto Token { get; set; }
    }
}