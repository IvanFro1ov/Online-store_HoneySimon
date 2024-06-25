using AutoMapper;
using Honey.DB.Entities;
using Honey.DB.Repository.Product;
using Honey.DB.Repository.ShopCart;
using Honey.Domain.Dto.Product;
using Honey.Domain.Dto.ShopCart;
using Honey.Domain.Interfaces;

namespace Honey.BL.Services;

/// <summary>
/// 
/// </summary>
public class ShopCartService : IShopCartService
{
    private readonly IShopCartRepository _shopCartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="shopCartRepository"></param>
    public ShopCartService(
        IShopCartRepository shopCartRepository,
        IMapper mapper, 
        IProductRepository productRepository)
    {
        _shopCartRepository = shopCartRepository;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<ShopCartResponseDto> GetUserShopCart(Guid userId)
    {
        var shopCart = _shopCartRepository.GetOne(p => p.UserId == userId);

        if (shopCart is null)
        {
            var newShopCart = new ShopCartEntity
            {
                UserId = userId,
                Products = new List<ProductEntity>(),
            };

            shopCart = await _shopCartRepository.Create(newShopCart);
        }
        
        var result = _mapper.Map<ShopCartResponseDto>(shopCart);
        return result;
    }

    public async Task<ShopCartResponseDto> AddProductToShopCart(Guid productId, Guid userId)
    {
        var currentProduct = _productRepository.GetOne(p => p.Id == productId);

        var shopCart = _shopCartRepository.GetOne(p => p.UserId == userId);
        
        shopCart.Products.Add(currentProduct);

        currentProduct.QuantityOfStock -= 1;

        await _shopCartRepository.Update(shopCart);

        await _productRepository.Update(currentProduct);

        var result = _mapper.Map<ShopCartResponseDto>(shopCart);

        return result;
    }

    public async Task<ShopCartResponseDto> RemoveProductInShopСart(Guid productId, Guid userId)
    {
        var shopCart = _shopCartRepository.GetOne(p => p.UserId == userId);

        var removableProduct = shopCart.Products.FirstOrDefault(p => p.Id == productId);

        if (removableProduct is not null)
        {
            shopCart.Products.Remove(removableProduct);

            await _shopCartRepository.Update(shopCart);
        }

        var result = _mapper.Map<ShopCartResponseDto>(shopCart);

        return result;
    }
}