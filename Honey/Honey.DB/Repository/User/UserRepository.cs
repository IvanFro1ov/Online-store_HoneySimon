using Honey.DB.Entities;
using Honey.DB.Repository.Base;
using Sixgram.Auth.Database;

namespace Honey.DB.Repository.User
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}