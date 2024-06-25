using Honey.DB.Entities;
using Honey.DB.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Honey.DB.Repository.Order;

public class OrderRepository : BaseRepository<OrderEntity>, IOrderRepository
{
    private readonly AppDbContext _context;
    
    public OrderRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<(IEnumerable<OrderEntity> data, int total)> GetPage(Guid userId,int skip, int take)
    {
        var query =  _context.Orders.Where(p => p.UserId == userId);

        var data = await query
            .OrderBy(p => p.DateCreated)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        var total = query.Count();
        
        return (data, total);
    }
}