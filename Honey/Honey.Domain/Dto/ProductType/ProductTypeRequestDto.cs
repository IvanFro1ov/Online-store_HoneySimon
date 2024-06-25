namespace Honey.Domain.Dto.ProductType;

public class ProductTypeRequestDto
{
    /// <summary>
    /// Название категории товаров
    /// </summary>
    public string CategoryName { get; set; }
    
    /// <summary>
    /// Описание категории
    /// </summary>
    public string Description { get; set; }
}