using System.ComponentModel.DataAnnotations.Schema;
using Honey.Common.Base;
using Honey.Common.Enums;

namespace Honey.DB.Entities;

/// <summary>
/// Модель заказа в БД
/// </summary>
public class OrderEntity : BaseModel
{
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
}