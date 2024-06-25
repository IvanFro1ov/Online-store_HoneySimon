using Honey.DB.Entities;
using Honey.DB.Repository.Base;

namespace Honey.DB.Repository.ProductType;

public interface IProductTypeRepository : IBaseRepository<ProductTypeEntity>
{
    Task<(IEnumerable<ProductTypeEntity> data, int total)> GetPage(int skip, int take);
}