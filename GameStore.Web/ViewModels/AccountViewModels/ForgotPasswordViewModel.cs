using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [EmailAddress(ErrorMessage = "Введенные данные не являются електронной почтой")]
        [Display(Name = "Email")]
        [MaxLength(100, ErrorMessage = "Длина {0} не может быть больше {1} символов")]
        public string Email { get; set; }
    }
}