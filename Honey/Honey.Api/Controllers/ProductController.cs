using System.Security.Claims;
using Honey.Domain.Dto.Product;
using Honey.Domain.Dto.User;
using Honey.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Guid;

namespace Honey.Controllers;

/// <summary>
/// Контроллер для взаимодействия с товарами
/// </summary>
[ApiController]
[Route("/api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IUserService _userService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="productService"></param>
    /// <param name="userService"></param>
    public ProductController(
        IProductService productService, 
        IUserService userService)
    {
        _productService = productService;
        _userService = userService;
    }

    /// <summary>
    /// Добавить новый товар
    /// </summary>
    /// <param name="requestDto">Модель запроса</param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddProduct([FromForm] ProductRequestDto requestDto)
    {
        var result = await _productService.CreateProduct(requestDto);

        return Ok(result);
    }

    /// <summary>
    /// Получение иконки
    /// </summary>
    /// <param name="productId">Идентификатор файла</param>
    /// <returns></returns>
    [HttpGet("icon")]
    [Authorize]
    public async Task<IActionResult> GetIcon(Guid productId)
    {
        var result = await _productService.GetFile(productId);

        return File(result.File, result.ContentType, "Icon");
    }

    /// <summary>
    /// Получение информации о товаре
    /// </summary>
    /// <param name="id">идентификатор продукта</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductInfo([FromQuery] Guid id)
    {
        var result = await _productService.GetProduct(id);

        return Ok(result);
    }

    /// <summary>
    /// Обновление информации о товаре
    /// </summary>
    /// <param name="id">идентификатор товара</param>
    /// <param name="requestDto">Модель запроса</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateProduct([FromQuery] Guid id, [FromBody] ProductRequestDto requestDto)
    {
        var result = await _productService.UpdateProduct(id, requestDto);

        return Ok(result);
    }

    /// <summary>
    /// Получение страницы товаров
    /// </summary>
    /// <param name="skip">Кол-во пропущенных элементов</param>
    /// <param name="take">Кол-во элементов в странице</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetList(int skip, int take)
    {
        var result = await _productService.GetPageProductType(skip, take);

        return Ok(result);
    }

    /// <summary>
    /// Удаление товара
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteProduct([FromQuery] Guid id)
    {
        await _productService.DeleteProduct(id);

        return Ok();
    }


    private async Task<UserModelResponseDto> GetCurrentUserById()
    {
        var test = HttpContext.User.FindFirst(p => p.Type == ClaimTypes.Name)?.Value;

        TryParse(test, out var userId);

        var result = await _userService.GetById(userId);

        return result;
    }
}