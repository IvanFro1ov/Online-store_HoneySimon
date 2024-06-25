using Honey.Domain.Dto.Product;

namespace Honey.Domain.Dto.ShopCart;

public class ShopCartResponseDto
{
    /// <summary>
    /// Информация о продуктах
    /// </summary>
    public ICollection<ProductResponseDto> Products { get; set; }
}