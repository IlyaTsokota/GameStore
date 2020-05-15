using GameStore.Web.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.WishViewModels
{
    public class WishViewModel
    {
        public int WishId { get; set; }

        public int ProductId { get; set; }

        public ProductViewModel Product { get; set; }


    }
}