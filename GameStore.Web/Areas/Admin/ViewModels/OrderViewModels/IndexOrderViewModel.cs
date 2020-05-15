using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Areas.Admin.ViewModels.OrderViewModels
{
    public class IndexOrderViewModel
    {
        public List<OrderViewModel> Orders { get; set; }

        public Pager Pager { get; set; }

        public int? OrderStatusId { get; set; }

        public string OnlyMyOrders { get; set; }

        public List<SelectListItem> OrderStatuses { get; set; }
    }
}