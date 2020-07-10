using System.Collections.Generic;

namespace GameStore.Web.ViewModels.ProductViewModels
{
    public class ProductFilterViewModel
    {
        public int CategoryId { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public int OrderBy { get; set; }

        public List<AttributeValueViewModel> CheckedAttributes { get; set; }
    }
}