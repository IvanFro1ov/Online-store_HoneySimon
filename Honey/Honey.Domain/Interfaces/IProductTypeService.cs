using Honey.Domain.Dto.ProductType;

namespace Honey.Domain.Interfaces;

public interface IProductTypeService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ProductTypeResponseDto> CreateProductType(ProductTypeRequestDto request);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    Task<ProductTypeResponseDto> UpdateProductType(Guid id, ProductTypeRequestDto requestDto);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ProductTypeResponseDto> GetProductType(Guid id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <returns></returns>
    Task<PageProductTypesResponseDto> GetPageProductType(int skip, int take);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteProductType(Guid id);
}