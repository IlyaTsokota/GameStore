using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.CartViewModels
{
    public class IndexCartViewModel
    {
        public List<CartViewModel> CartItems { get; set; }
        
        public double TotalValue { get; set; }
    }
}