using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.ProductViewModels
{
    public class IndexProductViewModel
    {

        public List<ProductViewModel> Products { get; set; }

        public Pager Pager { get; set; }

    }
}