using AutoMapper;
using GameStore.Model;
using GameStore.Web.ViewModels;
using GameStore.Web.ViewModels.CartViewModels;
using GameStore.Web.ViewModels.CategoryViewModels;
using GameStore.Web.ViewModels.OrderViewModels;
using GameStore.Web.ViewModels.ProductViewModels;
using GameStore.Web.ViewModels.ProfileViewModels;
using GameStore.Web.ViewModels.ReviewViewModels;
using GameStore.Web.ViewModels.WishViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Web.Mappings
{
    public class ToViewModel : Profile
    {
        public ToViewModel()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<ProductImage, ProductImageViewModel>();
            CreateMap<CartLine, CartViewModel>();
            CreateMap<Wish, WishViewModel>();

            CreateMap<User, ContactInfoViewModel>().ForMember(
              m => m.Phone, opt => opt.MapFrom(src => src.PhoneNumber));
            CreateMap<User, ProfileViewModel>().ForMember(
                m => m.UserInfo,
                opt => opt.MapFrom(src => Mapper.Map<User, ContactInfoViewModel>(src)));
            CreateMap<Review, ReviewViewModel>();
            CreateMap<Product, DetailsProductViewModel>().ForMember(
                   m => m.CategoryName,
                   opt => opt.MapFrom(src => src.Category.Name)).ForMember(
                   m => m.Images,
                   opt => opt.MapFrom(src => src.Images.Select(x => x.Image))).ForMember(
                   m => m.Characteristics,
                   opt => opt.MapFrom(src => src.AttributeValues
                       .Select(x => new KeyValuePair<string, string>(x.Attribute.Name, x.Value)))).ForMember(
                      m => m.Rating,
                      opt => opt.MapFrom(src => Math.Round(src.Reviews.Count != 0 ? src.Reviews.Average(x => x.Rating) : 0)))
                  .ForMember(m => m.ReviewCount, opt => opt.MapFrom(src => src.Reviews.Count)).ForMember(
                      m => m.NewReview,
                      opt => opt.MapFrom(x => new ReviewViewModel { ProductId = x.ProductId }));
            CreateMap<User, CreateOrderViewModel>().ForMember(
               m => m.Phone, opt => opt.MapFrom(src => src.PhoneNumber));
            CreateMap<OrderDetails, OrderDetailsViewModel>().ForMember(
                m => m.Name, opt => opt.MapFrom(src => src.Product.Name)).ForMember(
                m => m.QuantityInCart, opt => opt.MapFrom(src => src.Quantity));
            CreateMap<Order, OrderViewModel>().ForMember(
               m => m.OrderStatus,
               opt => opt.MapFrom(src => src.OrderStatus.Name));
            CreateMap<Order, DetailsOrderViewModel>().ForMember(
                m => m.OrderStatus,
                opt => opt.MapFrom(src => src.OrderStatus.Name)).ForMember(
                m => m.OrderDetails,
                opt => opt.MapFrom(
                    src => src.OrderDetails.Select(Mapper.Map<OrderDetails, OrderDetailsViewModel>)));

        }
    }
}