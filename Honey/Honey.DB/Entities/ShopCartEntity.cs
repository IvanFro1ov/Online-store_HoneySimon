using System.ComponentModel.DataAnnotations.Schema;
using Honey.Common.Base;

namespace Honey.DB.Entities;

/// <summary>
/// Модель крозины пользователя в БД
/// </summary>
public class ShopCartEntity : BaseModel
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Информация о продуктах
    /// </summary>
    [Column(TypeName = "jsonb")]
    public ICollection<ProductEntity> Products { get; set; }
    
    /// <summary>
    /// Fk to user
    /// </summary>
    [ForeignKey(nameof(UserId))]
    public UserEntity User { get; set; }
}