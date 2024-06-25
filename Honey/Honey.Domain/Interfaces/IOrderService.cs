using Honey.Common.Enums;
using Honey.Domain.Dto.Order;

namespace Honey.Domain.Interfaces;

/// <summary>
/// Интерфейс для взаимодействия с сервисом заказов
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Создать заказ
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns></returns>
    Task<OrderResponseDto> CreateOrder(Guid userId);

    /// <summary>
    /// Отменить заказ
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns></returns>
    Task<OrderResponseDto> CancelOrder(Guid orderId, Guid userId);

    /// <summary>
    /// Получение страницы заказов пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="skip">Кол-во пропущенных элементов</param>
    /// <param name="take">Кол-во элементов в странице</param>
    /// <returns></returns>
    Task<PageOrderResponseDto> GetPageUserOrders(Guid userId, int skip, int take);

    /// <summary>
    /// Получить информацию по заказу
    /// </summary>
    /// <param name="orderId">Иденьтификатор заказа</param>
    /// <returns></returns>
    Task<OrderResponseDto> GetOrderInfo(Guid orderId);

    /// <summary>
    /// Обновить статус заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="status">Статус заказа</param>
    /// <returns></returns>
    Task<OrderResponseDto> UpdateOrderStatus(Guid orderId, Statuses status);
}