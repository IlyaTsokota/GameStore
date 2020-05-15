using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.Areas.Admin.ViewModels.UserViewModels
{
    public class IndexUserViewModel
    {
        public List<UserViewModel> Users { get; set; }

        public Pager Pager { get; set; }
    }
}