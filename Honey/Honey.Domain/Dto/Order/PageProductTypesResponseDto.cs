using Honey.Domain.Dto.Product;

namespace Honey.Domain.Dto.Order;

/// <summary>
/// Модель ответа страницы типов продуктов
/// </summary>
public class PageOrderResponseDto
{
    /// <summary>
    /// Типы входящие в текущую страницу
    /// </summary>
    public IEnumerable<OrderResponseDto> Data { get; set; }
    
    /// <summary>
    /// Общее количество типов
    /// </summary>
    public int Total { get; set; }
}