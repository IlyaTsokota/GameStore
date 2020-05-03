using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Areas.Admin.ViewModels.SupplyViewModels
{
    public class IndexSupplyViewModel
    {
        public List<SupplyViewModel> Supplies { get; set; }

        public List<SelectListItem> Suppliers { get; set; }

        public Pager Pager { get; set; }
    }
}