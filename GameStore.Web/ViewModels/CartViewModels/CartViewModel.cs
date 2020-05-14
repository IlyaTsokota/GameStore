using GameStore.Web.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.CartViewModels
{
    public class CartViewModel
    {
        public int CartLineId { get; set; }

        public int ProductId { get; set; }

        public ProductViewModel Product { get; set; }

        public int Quantity { get; set; }
    }
}