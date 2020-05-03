using AutoMapper;
using GameStore.Model;
using GameStore.Web.Areas.Admin.ViewModels.AttributeValueViewModels;
using GameStore.Web.Areas.Admin.ViewModels.AttributeViewModels;
using GameStore.Web.Areas.Admin.ViewModels.CategoryViewModels;
using GameStore.Web.Areas.Admin.ViewModels.LogViewModels;
using GameStore.Web.Areas.Admin.ViewModels.ProductImageViewModel;
using GameStore.Web.Areas.Admin.ViewModels.ProductViewModel;
using GameStore.Web.Areas.Admin.ViewModels.SupplierViewModels;
using GameStore.Web.Areas.Admin.ViewModels.SupplyViewModels;
using System.Linq;

namespace GameStore.Web.Mappings
{
    public class AdminToViewModel : Profile
    {
        public AdminToViewModel()
        {
            CreateMap<Attribute, AttributeViewModel>();
            CreateMap<AttributeValue, AttributeValueViewModel>().ForMember(
                m => m.AttributeName,
                opt => opt.MapFrom(src => src.Attribute.Name));
            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, EditProductViewModel>();
            CreateMap<ProductImage, ProductImageViewModel>();
            CreateMap<Category, DetailsCategoryViewModel>().ForMember(
                m => m.Attributes,
                opt => opt.MapFrom(src => src.Attributes.Select(Mapper.Map<Attribute, AttributeViewModel>)));
            CreateMap<Log, LogViewModel>().ForMember(
               m => m.Message,
               opt => opt.MapFrom(p => p.Message.Substring(0, p.Message.Length > 200 ? 200 : p.Message.Length) + "..."));
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, EditCategoryViewModel>();
            CreateMap<Supplier, SupplierViewModel>();

            CreateMap<Supplier, DetailsSupplierViewModel>();
            CreateMap<Supply, SupplyViewModel>();
            CreateMap<SupplyProduct, SupplyProductViewModel>();
            CreateMap<Supply, DetailsSupplyViewModel>().ForMember(
                m => m.SupplyProductViewModels,
                opt => opt.MapFrom(p => p.SupplyProducts.Select(Mapper.Map<SupplyProduct, SupplyProductViewModel>)));
        }
    }
}