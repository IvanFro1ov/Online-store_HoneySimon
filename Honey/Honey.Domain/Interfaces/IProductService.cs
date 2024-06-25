using Honey.Domain.Dto.File;
using Honey.Domain.Dto.Product;

namespace Honey.Domain.Interfaces;

public interface IProductService
{
    /// <summary>
    /// Создание товара
    /// </summary>
    /// <param name="request">Модель запроса</param>
    /// <returns></returns>
    Task<ProductResponseDto> CreateProduct(ProductRequestDto request);

    /// <summary>
    /// Обновление сущности товара
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <param name="requestDto">Модель обновления</param>
    /// <returns></returns>
    Task<ProductResponseDto> UpdateProduct(Guid id, ProductRequestDto requestDto);

    /// <summary>
    /// Получение информации о продукте
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <returns></returns>
    Task<ProductResponseDto> GetProduct(Guid id);

    /// <summary>
    /// Получение фотографии файла
    /// </summary>
    /// <param name="productId">Идентификатор товара</param>
    /// <returns></returns>
    Task<FileModel> GetFile(Guid productId);

    /// <summary>
    /// Получить страницу товаров
    /// </summary>
    /// <param name="skip">Кол-во пропускаемых элементов</param>
    /// <param name="take">Кол-во элементов с странице</param>
    /// <returns></returns>
    Task<PageProductResponseDto> GetPageProductType(int skip, int take);
    
    /// <summary>
    /// Удаление товара
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <returns></returns>
    Task DeleteProduct(Guid id);
}