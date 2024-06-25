namespace Honey.Common.Enums;

public enum Statuses
{
    /// <summary>
    /// Создан
    /// </summary>
    Created = 1,
    
    /// <summary>
    /// В обработке
    /// </summary>
    Assembled = 2,
    
    /// <summary>
    /// Передан в доставку
    /// </summary>
    ToDelivery = 3,
    
    /// <summary>
    /// Доставляется
    /// </summary>
    Delivered = 4,
    
    /// <summary>
    /// Доставлен
    /// </summary>
    Received = 5,
    
    /// <summary>
    /// Отменен
    /// </summary>
    Canceled = 6
}