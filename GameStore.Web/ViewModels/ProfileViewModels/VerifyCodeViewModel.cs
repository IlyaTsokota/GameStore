using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.ProfileViewModels
{
    public class VerifyCodeViewModel
    {

        [Required(ErrorMessage = "Это поле не должно быть пустым")]
        [Display(Name = "Провайдер")]
        public string Provider { get; set; }


        [Required(ErrorMessage = "Это поле не должно быть пустым")]
        [Display(Name = "Код")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Запомнить браузером?")]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Запомнить вас?")]
        public bool RememberMe { get; set; }
    }
}