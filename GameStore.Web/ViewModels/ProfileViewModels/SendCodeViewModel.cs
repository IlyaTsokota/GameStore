using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.ProfileViewModels
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }

        public string ReturnUrl { get; set; }

        public string Email { get; set; }

        [DisplayName("Запомнить вас?")]
        public bool RememberMe { get; set; }
    }
}