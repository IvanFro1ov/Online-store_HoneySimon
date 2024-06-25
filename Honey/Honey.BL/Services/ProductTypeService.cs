using AutoMapper;
using Honey.DB.Entities;
using Honey.DB.Repository.ProductType;
using Honey.Domain.Dto.ProductType;
using Honey.Domain.Interfaces;

namespace Honey.BL.Services;

/// <summary>
/// Сервис для вхаимодействия с типами товаров
/// </summary>
public class ProductTypeService : IProductTypeService
{
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="productTypeRepository"></param>
    public ProductTypeService(IProductTypeRepository productTypeRepository, IMapper mapper)
    {
        _productTypeRepository = productTypeRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Создать новый тип продукта
    /// </summary>
    /// <param name="request">Модель запроса</param>
    /// <returns></returns>
    public async Task<ProductTypeResponseDto> CreateProductType(ProductTypeRequestDto request)
    {
        var productType = _productTypeRepository.GetOne(p => p.CategoryName == request.CategoryName);

        if (productType is not null)
        {
            return null;
        }

        var productTypeEntity = new ProductTypeEntity
        {
            CategoryName = request.CategoryName,
            Description = request.Description
        };

        var created = await _productTypeRepository.Create(productTypeEntity);

        var result = _mapper.Map<ProductTypeResponseDto>(created);

        return result;
    }

    /// <summary>
    /// Обновление типа продукта
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="requestDto">Модель запроса</param>
    /// <returns></returns>
    public async Task<ProductTypeResponseDto> UpdateProductType(Guid id ,ProductTypeRequestDto requestDto)
    {
        var productType = await _productTypeRepository.GetById(id);

        if (productType is null)
        {
            return null;
        }

        productType.CategoryName = requestDto.CategoryName;
        productType.Description = requestDto.Description;
        productType.DateUpdated = DateTime.UtcNow;

        await _productTypeRepository.Update(productType);

        return _mapper.Map<ProductTypeResponseDto>(productType);
    }

    /// <summary>
    /// Получение типа продукта
    /// </summary>
    /// <param name="id">Идентификатор типа продукта</param>
    /// <returns></returns>
    public async Task<ProductTypeResponseDto> GetProductType(Guid id)
    {
        var productType = await _productTypeRepository.GetById(id);

        if (productType is null)
        {
            return null;
        }

        var result = _mapper.Map<ProductTypeResponseDto>(productType);

        return result;
    }

    /// <summary>
    /// Получение страницы типов продуктов
    /// </summary>
    /// <param name="skip">Сколько пропустить</param>
    /// <param name="take">Сколько элементов взять в странице</param>
    /// <returns></returns>
    public async Task<PageProductTypesResponseDto> GetPageProductType(int skip, int take)
    {
        var page = await _productTypeRepository.GetPage(skip, take);

        var result = _mapper.Map<IEnumerable<ProductTypeResponseDto>>(page.data);

        return new PageProductTypesResponseDto
        {
            Data = result,
            Total = page.total
        };
    }
    
    /// <summary>
    /// Удаление типа продукта
    /// </summary>
    /// <param name="id">Идетификатор типа продукта</param>
    public async Task DeleteProductType(Guid id)
    {
        await _productTypeRepository.Delete(id);
    }
}