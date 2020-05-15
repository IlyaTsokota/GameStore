using AutoMapper;
using GameStore.Model;
using GameStore.Web.Areas.Admin.ViewModels.AttributeValueViewModels;
using GameStore.Web.Areas.Admin.ViewModels.AttributeViewModels;
using GameStore.Web.Areas.Admin.ViewModels.CategoryViewModels;
using GameStore.Web.Areas.Admin.ViewModels.ProductViewModel;
using GameStore.Web.Areas.Admin.ViewModels.SupplierViewModels;
using GameStore.Web.Areas.Admin.ViewModels.SupplyViewModels;
using GameStore.Web.Extensions;
using System.Linq;

namespace GameStore.Web.Mappings
{
    public class AdminFromViewModel : Profile
    {
        public AdminFromViewModel()
        {
            CreateMap<AttributeViewModel, Attribute>();

            CreateMap<AttributeValueViewModel, AttributeValue>();
            CreateMap<CreateCategoryViewModel, Category>().ForMember(
                m => m.Image,
                opt => opt.MapFrom(p => p.CreateImage.ToByteArray()));
            CreateMap<EditCategoryViewModel, Category>().ForMember(
            m => m.Image,
            opt => opt.MapFrom(p => p.EditImage.ToByteArray()));
            CreateMap<SupplierViewModel, Supplier>();
            CreateMap<CreateProductViewModel, Product>().ForMember(
              m => m.Images,
              opt => opt.MapFrom(src => src.Images
                  .Select(x => new ProductImage { Image = x.ToByteArray(), ProductId = src.ProductId })));
            CreateMap<EditProductViewModel, Product>();

            CreateMap<SupplyProductViewModel, SupplyProduct>();
            CreateMap<AddSupplyViewModel, Supply>();
        }
    }
}