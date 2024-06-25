using Honey.DB.Entities;
using Honey.DB.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Honey.DB.Repository.Product;

public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
{
    private readonly AppDbContext _context;
    
    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<(IEnumerable<ProductEntity> data, int total)> GetPage(int skip, int take)
    {
        var query =  _context.Products;

        var data = await query.OrderBy(p => p.DateCreated).Skip(skip).Take(take).ToListAsync();

        var total = query.Count();
        
        return (data, total);
    }
}