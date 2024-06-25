using Honey.DB.Entities;

namespace Honey.Domain.Dto.ShopCart;

/// <summary>
/// 
/// </summary>
public class ShopCartRequestDto
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Продукты в корзине пользователя
    /// </summary>
    public IEnumerable<ProductEntity> Products { get; set; }
}