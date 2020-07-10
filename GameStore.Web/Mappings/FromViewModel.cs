using AutoMapper;
using GameStore.Model;
using GameStore.Web.ViewModels.OrderViewModels;
using GameStore.Web.ViewModels.ProductViewModels;
using GameStore.Web.ViewModels.ReviewViewModels;

namespace GameStore.Web.Mappings
{
    public class FromViewModel : Profile
    {
        public FromViewModel()
        {
            CreateMap<AttributeValueViewModel, AttributeValue>();
            CreateMap<CategoryViewModel, Category>();
            CreateMap<CreateOrderViewModel, Order>();
            CreateMap<ReviewViewModel, Review>();

        }
    }
}