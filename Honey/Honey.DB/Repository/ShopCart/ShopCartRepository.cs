using Honey.DB.Entities;
using Honey.DB.Repository.Base;

namespace Honey.DB.Repository.ShopCart;

public class ShopCartRepository : BaseRepository<ShopCartEntity>, IShopCartRepository
{
    public ShopCartRepository(AppDbContext context) : base(context)
    {
        
    }
}