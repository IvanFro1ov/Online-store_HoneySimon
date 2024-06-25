using Honey.Common.Enums;
using Honey.DB.Entities;

namespace Honey.Domain.Dto.Order;

/// <summary>
/// Модель приходящая с фронта
/// </summary>
public class OrderRequestDto
{
    /// <summary>
    /// Идентифтикатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Информация о товарах
    /// </summary>
    public IEnumerable<ProductEntity> Products { get; set; }
    
    /// <summary>
    /// Статус заказа
    /// </summary>
    public Statuses Status { get; set; }
}