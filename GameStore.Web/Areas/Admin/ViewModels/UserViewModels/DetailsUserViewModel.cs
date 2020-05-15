using GameStore.Web.Areas.Admin.ViewModels.OrderViewModels;

using System.Collections.Generic;


namespace GameStore.Web.Areas.Admin.ViewModels.UserViewModels
{
    public class DetailsUserViewModel : UserViewModel
    {
        public List<OrderViewModel> Orders { get; set; }
    }
}