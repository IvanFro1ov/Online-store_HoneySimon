using Honey.Domain.Dto.ProductType;
using Honey.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Honey.Controllers;

/// <summary>
/// Контроллер для типов продуктов
/// </summary>
[ApiController]
[Route("/api/[controller]")]
public class ProductTypeController : ControllerBase
{
    private readonly IProductTypeService _productTypeService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="productTypeService"></param>
    public ProductTypeController(IProductTypeService productTypeService)
    {
        _productTypeService = productTypeService;
    }

    /// <summary>
    /// Добавить новый тип продуктов
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddProductType(ProductTypeRequestDto requestDto)
    {
        var result = await _productTypeService.CreateProductType(requestDto);

        return Ok(result);
    }

    /// <summary>
    /// Обновить существующий тип продуктов
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductType(Guid id, ProductTypeRequestDto requestDto)
    {
        var result = await _productTypeService.UpdateProductType(id, requestDto);

        return Ok(result);
    }

    /// <summary>
    /// Получить тип продукта по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductType(Guid id)
    {
        var result = await _productTypeService.GetProductType(id);

        return Ok(result);
    }

    /// <summary>
    /// Получить страницу продуктов
    /// </summary>
    /// <param name="skip">Сколько пропустить</param>
    /// <param name="take">Сколько взять</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetProductTypeList(int skip, int take)
    {
        var result = await _productTypeService.GetPageProductType(skip, take);

        return Ok(result);
    }

    /// <summary>
    /// Удаление типа продукта
    /// </summary>
    /// <param name="id">Идентификатор типа продукта</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductType(Guid id)
    {
        await _productTypeService.DeleteProductType(id);

        return Ok();
    } 
}