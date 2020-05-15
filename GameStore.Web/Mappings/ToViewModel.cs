using AutoMapper;
using GameStore.Model;
using GameStore.Web.ViewModels.CartViewModels;
using GameStore.Web.ViewModels.CategoryViewModels;
using GameStore.Web.ViewModels.ProductViewModels;
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
        }
    }
}