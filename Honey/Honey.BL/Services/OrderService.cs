using AutoMapper;
using Honey.Common.Enums;
using Honey.DB.Entities;
using Honey.DB.Repository.Order;
using Honey.DB.Repository.ShopCart;
using Honey.Domain.Dto.Order;
using Honey.Domain.Interfaces;

namespace Honey.BL.Services;

/// <summary>
/// Сервис заказов
/// </summary>
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IShopCartRepository _shopCartRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор он и в африке конструктор
    /// </summary>
    public OrderService(
        IOrderRepository orderRepository, 
        IShopCartRepository shopCartRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _shopCartRepository = shopCartRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Создание заказа
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns></returns>
    public async Task<OrderResponseDto> CreateOrder(Guid userId)
    {
        var userShopCart = _shopCartRepository.GetOne(p => p.UserId == userId);

        if (userShopCart?.Products?.Any() is false)
        {
            return null;
        }

        var newOrder = new OrderEntity
        {
            UserId = userId,
            Products = userShopCart?.Products,
            Status = Statuses.Created
        };

        await _orderRepository.Create(newOrder);

        var result = _mapper.Map<OrderResponseDto>(newOrder);

        return result;
    }
    
    /// <summary>
    /// Отменить заказ
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns></returns>
    public async Task<OrderResponseDto> CancelOrder(Guid orderId, Guid userId)
    {
        var orderEntity = _orderRepository.GetOne(p =>p.Id == orderId && p.UserId == userId);

        if (orderEntity is null)
        {
            return null;
        }

        orderEntity.Status = Statuses.Canceled;
        orderEntity.DateUpdated = DateTime.UtcNow;
        
        await _orderRepository.Update(orderEntity);

        var result = _mapper.Map<OrderResponseDto>(orderEntity);

        return result;
    }

    /// <summary>
    /// Получить историю заказов пользователя
    /// </summary>
    /// <param name="userId">Идентифкиатор пользователя</param>
    /// <param name="skip">Кол-во пропущенных элементов</param>
    /// <param name="take">Кол-во элементов в странице</param>
    /// <returns></returns>
    public async Task<PageOrderResponseDto> GetPageUserOrders(Guid userId, int skip, int take)
    {
        var orders = await _orderRepository.GetPage(userId, skip, take);

        var result = new PageOrderResponseDto
        {
            Data = _mapper.Map<IEnumerable<OrderResponseDto>>(orders.data),
            Total = orders.total
        };

        return result;
    }

    /// <summary>
    /// Получение информации по определенному заказу
    /// </summary>
    /// <param name="orderId">Идентифкиатор заказа</param>
    /// <returns></returns>
    public async Task<OrderResponseDto> GetOrderInfo(Guid orderId)
    {
        var orderEntity = await _orderRepository.GetById(orderId);

        var result = _mapper.Map<OrderResponseDto>(orderEntity);

        return result;
    }
    
    /// <summary>
    /// Обновить статус заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="status">Статус заказа</param>
    /// <returns></returns>
    public async Task<OrderResponseDto> UpdateOrderStatus(Guid orderId, Statuses status)
    {
        var orderEntity = await _orderRepository.GetById(orderId);

        if (orderEntity is null)
        {
            return null;
        }

        orderEntity.Status = status;

        await _orderRepository.Update(orderEntity);

        var result = _mapper.Map<OrderResponseDto>(orderEntity);

        return result;
    }
}