using AutoMapper;
using Honey.DB.Entities;
using Honey.DB.Repository.Product;
using Honey.Domain.Dto.File;
using Honey.Domain.Dto.Product;
using Honey.Domain.Interfaces;
using Microsoft.AspNetCore.Http.Features;

namespace Honey.BL.Services;

/// <summary>
/// Сервис для взаимодействия с товарами
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="productRepository"></param>
    /// <param name="mapper"></param>
    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Создать новый товар
    /// </summary>
    /// <param name="request">Модель запроса</param>
    /// <returns></returns>
    public async Task<ProductResponseDto> CreateProduct(ProductRequestDto request)
    {
        var product = _productRepository.GetOne(p => p.Name == request.Name);

        if (product is not null)
        {
            return null;
        }

        byte[] file;

        using (var fileStream = new MemoryStream())
        {
            await request.Icon.CopyToAsync(fileStream); 
            
            file = fileStream.ToArray();
        }

        var productEntity = new ProductEntity
        {
            Name = request.Name,
            ShortDesc = request.ShortDesc,
            Description = request.Description,
            Price = request.Price,
            ProductTypeId = request.ProductTypeId,
            QuantityOfStock = request.QuantityOfStock,
            File = file,
            ContentType = request.Icon.ContentType,
            Actual = request.Actual
        };

        var created = await _productRepository.Create(productEntity);

        var result = _mapper.Map<ProductResponseDto>(created);

        return result;
    }

    public async Task<FileModel> GetFile(Guid productId)
    {
        var product = await _productRepository.GetById(productId);

        var stream = new MemoryStream();

        stream.ReadAsync(product.File);

        return new FileModel
        {
            File = stream,
            ContentType = product.ContentType
        };
    }
    
    /// <summary>
    /// Получение информации о товаре
    /// </summary>
    /// <param name="id">Идентификатор типа продукта</param>
    /// <returns></returns>
    public async Task<ProductResponseDto> GetProduct(Guid id)
    {
        var productEntity = await _productRepository.GetById(id);

        if (productEntity is null)
        {
            return null;
        }

        var result = _mapper.Map<ProductResponseDto>(productEntity);

        return result;
    }
    
    /// <summary>
    /// Обновление товара
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="requestDto">Модель запроса</param>
    /// <returns></returns>
    public async Task<ProductResponseDto> UpdateProduct(Guid id, ProductRequestDto requestDto)
    {
        var productEntity = await _productRepository.GetById(id);

        if (productEntity is null)
        {
            return null;
        }

        productEntity.ProductTypeId = requestDto.ProductTypeId;
        productEntity.Description = requestDto.Description;
        productEntity.Actual = requestDto.Actual;
        productEntity.ShortDesc = requestDto.ShortDesc;
        productEntity.Name = requestDto.Name;
        productEntity.Price = requestDto.Price;
        productEntity.QuantityOfStock = requestDto.QuantityOfStock;
        productEntity.DateUpdated = DateTime.UtcNow;

        await _productRepository.Update(productEntity);

        return _mapper.Map<ProductResponseDto>(productEntity);
    }
    
    /// <summary>
    /// Получение страницы товаров
    /// </summary>
    /// <param name="skip">Сколько пропустить</param>
    /// <param name="take">Сколько взять в странице</param>
    /// <returns></returns>
    public async Task<PageProductResponseDto> GetPageProductType(int skip, int take)
    {
        var page = await _productRepository.GetPage(skip, take);

        var result = _mapper.Map<IEnumerable<ProductResponseDto>>(page.data);

        return new PageProductResponseDto
        {
            Data = result,
            Total = page.total
        };
    }
    
    /// <summary>
    /// Удаление товара
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    public async Task DeleteProduct(Guid id)
    {
        await _productRepository.Delete(id);
    }
}