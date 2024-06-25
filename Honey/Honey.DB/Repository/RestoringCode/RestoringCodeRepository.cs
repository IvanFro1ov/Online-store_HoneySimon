using Honey.DB.Entities;
using Honey.DB.Repository.Base;
using Sixgram.Auth.Database;

namespace Honey.DB.Repository.RestoringCode
{
    public class RestoringCodeRepository : BaseRepository<RestoringCodeEntity>, IRestoringCodeRepository
    {
        public RestoringCodeRepository(AppDbContext context) : base(context)
        {
        }
    }
}