namespace Honey.Domain.Dto.ProductType;

public class ProductTypeResponseDto
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public Guid Id { get; set;}
    
    /// <summary>
    /// Название категории товаров
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    /// Описание категории
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Дата создания сущности
    /// </summary>
    public DateTime DateCreated { get; set; }
}