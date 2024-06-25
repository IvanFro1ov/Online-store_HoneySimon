namespace Honey.Domain.Dto.Product;

/// <summary>
/// Модель ответа страницы типов продуктов
/// </summary>
public class PageProductResponseDto
{
    /// <summary>
    /// Типы входящие в текущую страницу
    /// </summary>
    public IEnumerable<ProductResponseDto> Data { get; set; }
    
    /// <summary>
    /// Общее количество типов
    /// </summary>
    public int Total { get; set; }
}