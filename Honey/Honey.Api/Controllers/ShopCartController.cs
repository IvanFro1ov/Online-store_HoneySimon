using System.Security.Claims;
using Honey.Domain.Dto.User;
using Honey.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Honey.Controllers;

/// <summary>
/// Контроллер
/// </summary>
[ApiController]
[Authorize]
[Route("/api/[controller]")]
public class ShopCartController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IShopCartService _shopCartService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService"></param>
    public ShopCartController(
        IUserService userService, 
        IShopCartService shopCartService)
    {
        _userService = userService;
        _shopCartService = shopCartService;
    }

    /// <summary>
    /// Получить корзину пользователя
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetUserShopCart()
    {
        var userId = GetCurrentUserId();

        var result = await _shopCartService.GetUserShopCart(userId);

        return Ok(result);
    }

    /// <summary>
    /// Добавить товар в корзину
    /// </summary>
    /// <returns></returns>
    [HttpPut("add")]
    public async Task<IActionResult> AddProductToshopCart([FromBody] Guid productId)
    {
        var userId = GetCurrentUserId();
        
        var result = await _shopCartService.AddProductToShopCart(productId,userId);

        return Ok(result);
    }

    /// <summary>
    /// Удаление продукта из корзины
    /// </summary>
    [HttpPut("remove")]
    public async Task<IActionResult> RemoveProductInShopCart([FromBody] Guid productId)
    {
        var userId = GetCurrentUserId();

        var result = await _shopCartService.RemoveProductInShopСart(productId, userId);

        return Ok(result);
    }
    
    private async Task<UserModelResponseDto> GetCurrentUserById()
    {
        var userId = GetCurrentUserId();

        var result = await _userService.GetById(userId);

        return result;
    }

    private Guid GetCurrentUserId()
    {
        var test = HttpContext.User.FindFirst(p => p.Type == ClaimTypes.Name)?.Value;

        Guid.TryParse(test, out var userId);

        return userId;
    }
}