using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Areas.Admin.ViewModels.ProductViewModel
{
    public class IndexProductViewModel
    {
        public List<ProductViewModel> Products { get; set; }

        public Pager Pager { get; set; }

        public List<SelectListItem> Categories { get; set; }
    }
}