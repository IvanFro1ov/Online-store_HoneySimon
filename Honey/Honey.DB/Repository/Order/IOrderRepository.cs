using Honey.DB.Entities;
using Honey.DB.Repository.Base;

namespace Honey.DB.Repository.Order;

public interface IOrderRepository : IBaseRepository<OrderEntity>
{
    Task<(IEnumerable<OrderEntity> data, int total)> GetPage(Guid userId, int skip, int take);
}