using Honey.Common.Base;

namespace Honey.DB.Entities;

/// <summary>
/// Модель категории товара в БД
/// </summary>
public class ProductTypeEntity : BaseModel
{
    /// <summary>
    /// Название категории товаров
    /// </summary>
    public string CategoryName { get; set; }
    
    /// <summary>
    /// Описание категории
    /// </summary>
    public string Description { get; set; }
    
    public IEnumerable<ProductEntity> Products { get; set; } 
}