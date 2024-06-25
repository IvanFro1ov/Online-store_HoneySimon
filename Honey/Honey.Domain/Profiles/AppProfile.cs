using AutoMapper;
using Honey.DB.Entities;
using Honey.Domain.Dto.Authentication.Login;
using Honey.Domain.Dto.Authentication.Register;
using Honey.Domain.Dto.Authentication.RestoringPassword.ForgotPassword;
using Honey.Domain.Dto.Authentication.RestoringPassword.RestorePassword;
using Honey.Domain.Dto.Order;
using Honey.Domain.Dto.Product;
using Honey.Domain.Dto.ProductType;
using Honey.Domain.Dto.ShopCart;
using Honey.Domain.Dto.Token;
using Honey.Domain.Dto.User;
using Honey.Domain.Dto.User.Update;
using Sixgram.Auth.Core.Token;

namespace Honey.Domain.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<UserEntity, UserModelDto>();
            CreateMap<UserEntity, UserLoginResponseDto>();
            CreateMap<UserEntity, UserRegisterResponseDto>();
            CreateMap<UserEntity, UserModelResponseDto>();
            CreateMap<UserEntity, UserUpdateResponseDto>();
            CreateMap<UserEntity, UserLoginResponseDto>();
            CreateMap<UserEntity, UserRegisterResponseDto>();
            CreateMap<UserEntity, UserModelResponseDto>();
            CreateMap<UserEntity, UserUpdateResponseDto>();

            CreateMap<TokenModel, TokenModelDto>();

            CreateMap<UserRegisterRequestDto, UserEntity>();
            CreateMap<UserRegisterRequestDto, UserModelDto>();

            CreateMap<ForgotPasswordRequestDto, ForgotPasswordResponseDto>();
            CreateMap<ForgotPasswordRequestDto, ForgotPasswordResponseDto>();
            
            CreateMap<RestorePasswordRequestDto, RestorePasswordResponseDto>();
            CreateMap<RestorePasswordRequestDto, RestorePasswordResponseDto>();

            CreateMap<ProductTypeEntity, ProductTypeResponseDto>().ReverseMap();

            CreateMap<ProductEntity, ProductResponseDto>().ReverseMap();

            CreateMap<OrderEntity, OrderResponseDto>();

            CreateMap<ShopCartEntity, ShopCartResponseDto>().ReverseMap();
        }
    }
}