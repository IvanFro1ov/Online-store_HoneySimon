namespace Honey.Domain.Dto.Product;

public class ProductResponseDto
{
    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public Guid Id { get; set; }
    
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
    /// Актуален ли товар
    /// </summary>
    public bool Actual { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime DateCreated { get; set; }
    
    /// <summary>
    /// Дата обновления
    /// </summary>
    public DateTime? DateUpdated { get; set; }
}