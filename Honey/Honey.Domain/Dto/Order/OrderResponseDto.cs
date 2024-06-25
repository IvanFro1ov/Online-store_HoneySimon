using System.ComponentModel.DataAnnotations.Schema;
using Honey.Common.Enums;
using Honey.DB.Entities;

namespace Honey.Domain.Dto.Order;

public class OrderResponseDto
{
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентифтикатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Информация о товарах
    /// </summary>
    [Column(TypeName = "jsonb")]
    public IEnumerable<ProductEntity> Products { get; set; }
    
    /// <summary>
    /// Статус заказа
    /// </summary>
    public Statuses Status { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime DateCreated { get; set; }
    
    /// <summary>
    /// Дата последнего обновления
    /// </summary>
    public DateTime? DateUpdated { get; set; }
}