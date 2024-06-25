using Honey.DB.Entities;
using Honey.DB.Repository.Base;
using Honey.DB.Repository.Product;
using Microsoft.EntityFrameworkCore;

namespace Honey.DB.Repository.ProductType;

public class ProductTypeRepository : BaseRepository<ProductTypeEntity>, IProductTypeRepository
{
    private readonly AppDbContext _context;
    
    public ProductTypeRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<ProductTypeEntity> data, int total)> GetPage(int skip, int take)
    {
        var query =  _context.ProductTypes;

        var data = await query.OrderBy(p => p.DateCreated).Skip(skip).Take(take).ToListAsync();

        var total = query.Count();
        
        return (data, total);
    }
}