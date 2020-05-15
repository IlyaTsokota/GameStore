using GameStore.Web.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.HomeViewModels
{
    public class HomeIndexViewModel
    {
        public List<ProductViewModel> PlaystationList { get; set; }


        public List<ProductViewModel> XboxList { get; set; }


        public List<ProductViewModel> AllProducts { get; set; }
    }
}