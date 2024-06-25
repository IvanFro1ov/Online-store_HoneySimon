using System.Security.Claims;
using Honey.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Honey.Controllers;

/// <summary>
/// Контроллер для взаимодействия с заказом
/// </summary>
[ApiController]
[Authorize]
[Route("/api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    /// <summary>
    /// Контроллер
    /// </summary>
    /// <param name="orderService"></param>
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Создать заказ
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateOrder()
    {
        var userId = GetCurrentUserId();

        var result = await _orderService.CreateOrder(userId);

        return Ok(result);
    }

    /// <summary>
    /// Отменить заказ 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        var userId = GetCurrentUserId();

        var result = await _orderService.CancelOrder(id, userId);

        return Ok(result);
    }

    /// <summary>
    /// Получить информацию о заказе
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderInfo(Guid id)
    {
        var result = await _orderService.GetOrderInfo(id);

        return Ok(result);
    }

    /// <summary>
    /// Получить страницу заказов
    /// </summary>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetOrderList([FromQuery] int skip, [FromQuery] int take)
    {
        var userId = GetCurrentUserId();

        var result = await _orderService.GetPageUserOrders(userId, skip, take);

        return Ok(result);
    }
    
    private Guid GetCurrentUserId()
    {
        var test = HttpContext.User.FindFirst(p => p.Type == ClaimTypes.Name)?.Value;

        Guid.TryParse(test, out var userId);

        return userId;
    }
}