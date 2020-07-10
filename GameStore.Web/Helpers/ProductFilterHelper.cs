using AutoMapper;
using GameStore.Model;
using GameStore.Service.ProductFilters;
using GameStore.Web.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
namespace GameStore.Web.Helpers
{
    public static class ProductFilterHelper
    {
        public static List<IProductFilter> GetFilters(ProductFilterViewModel model)
        {
            var filters = new List<IProductFilter> { new ProductCategoryFilter(model.CategoryId) };
            if (model.MaxPrice >= model.MinPrice)
            {
                filters.Add(new ProductPriceFilter(model.MinPrice, model.MaxPrice));
            }

            if (model.CheckedAttributes != null)
            {
                var checkedAttributes = model.CheckedAttributes.Select(Mapper.Map<AttributeValueViewModel, AttributeValue>);
                filters.Add(new ProductAttributeValueFilter(checkedAttributes));
            }

            return filters;
        }
    }
}