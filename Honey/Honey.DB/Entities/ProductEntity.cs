using System.ComponentModel.DataAnnotations.Schema;
using Honey.Common.Base;

namespace Honey.DB.Entities;

/// <summary>
/// Модель товара в БД
/// </summary>
public class ProductEntity : BaseModel
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
    /// Файл
    /// </summary>
    public byte[] File { get; set; }
    
    /// <summary>
    /// Тип файла
    /// </summary>
    public string ContentType { get; set; }
    
    /// <summary>
    /// Актуален ли товар
    /// </summary>
    public bool Actual { get; set; }
    
    [ForeignKey(nameof(ProductTypeId))]
    public ProductTypeEntity ProductType { get; set; }
}