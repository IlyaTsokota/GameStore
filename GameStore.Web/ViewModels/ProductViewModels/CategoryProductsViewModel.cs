using GameStore.Model;
using GameStore.Web.Models;
using System.Collections.Generic;

namespace GameStore.Web.ViewModels.ProductViewModels
{
    public class CategoryProductsViewModel : CategoryViewModel
    {
        public ProductFilterViewModel ProductFilterViewModel { get; set; }

        public List<Attribute> Attributes { get; set; }

        public PagerViewModel<ProductViewModel> Products { get; set; }

        public int Page { get; set; }
    }
}