namespace Honey.Domain.Dto.ProductType;

/// <summary>
/// Модель ответа страницы типов продуктов
/// </summary>
public class PageProductTypesResponseDto
{
    /// <summary>
    /// Типы входящие в текущую страницу
    /// </summary>
    public IEnumerable<ProductTypeResponseDto> Data { get; set; }
    
    /// <summary>
    /// Общее количество типов
    /// </summary>
    public int Total { get; set; }
}