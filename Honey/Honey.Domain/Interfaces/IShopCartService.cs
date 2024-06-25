using Honey.Domain.Dto.ShopCart;

namespace Honey.Domain.Interfaces;

/// <summary>
/// Интерфейс для взаимодействия с сервисом корзины
/// </summary>
public interface IShopCartService
{
    /// <summary>
    /// Получить козину покупок пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns></returns>
    Task<ShopCartResponseDto> GetUserShopCart(Guid userId);

    /// <summary>
    /// Добавление товара в корзину
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns></returns>
    Task<ShopCartResponseDto> AddProductToShopCart(Guid productId, Guid userId);
    
    /// <summary>
    /// Удалить товар из корзины
    /// </summary>
    /// <param name="productId">Идентификатор продукта</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns></returns>
    Task<ShopCartResponseDto> RemoveProductInShopСart(Guid productId, Guid userId);
}