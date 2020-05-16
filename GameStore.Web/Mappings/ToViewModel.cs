using AutoMapper;
using GameStore.Model;
using GameStore.Web.ViewModels;
using GameStore.Web.ViewModels.CartViewModels;
using GameStore.Web.ViewModels.CategoryViewModels;
using GameStore.Web.ViewModels.ProductViewModels;
using GameStore.Web.ViewModels.ProfileViewModels;
using GameStore.Web.ViewModels.WishViewModels;

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
        }
    }
}