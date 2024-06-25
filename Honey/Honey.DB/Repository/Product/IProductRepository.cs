using Honey.DB.Entities;
using Honey.DB.Repository.Base;

namespace Honey.DB.Repository.Product;

public interface IProductRepository : IBaseRepository<ProductEntity>
{
    /// <summary>
    /// Получить страницу товаров
    /// </summary>
    /// <param name="skip">Сколько пропустить</param>
    /// <param name="take">Сколько получить</param>
    /// <returns></returns>
    Task<(IEnumerable<ProductEntity> data, int total)> GetPage(int skip, int take);
}