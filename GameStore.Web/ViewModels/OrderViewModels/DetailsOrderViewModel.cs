using GameStore.Web.ViewModels.LiqPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.OrderViewModels
{
    public class DetailsOrderViewModel : OrderViewModel
    {
        public string Comment { get; set; }

        public List<OrderDetailsViewModel> OrderDetails { get; set; }

        public LiqPayCheckoutFormModel LiqPayCheckoutFormModel { get; set; }
    }
}