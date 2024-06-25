using Honey.Common.Base;
using Honey.Common.Enums.Roles;

namespace Honey.Domain.Dto.User
{
    public class UserModelDto : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        
        public UserRoles Role { get; set; }
    }
}