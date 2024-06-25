using Microsoft.AspNetCore.Http;

namespace Honey.Domain.Dto.Product;

/// <summary>
/// Модель приходящая извне
/// </summary>
public class ProductRequestDto
{
    /// <summary>
    /// Название товара
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Короткое описание
    /// </summary>
    public string ShortDesc { get; set; }
    
    /// <summary>
    /// ПОлное описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Цена
    /// </summary>
    public double Price { get; set; }
    
    /// <summary>
    /// Идентификатор категории товара
    /// </summary>
    public Guid ProductTypeId { get; set; }
    
    /// <summary>
    /// Количество в наличии
    /// </summary>
    public int QuantityOfStock { get; set; }
    
    /// <summary>
    /// Фото товара
    /// </summary>
    public IFormFile Icon { get; set; }
    
    /// <summary>
    /// Актуален ли товар
    /// </summary>
    public bool Actual { get; set; }
}