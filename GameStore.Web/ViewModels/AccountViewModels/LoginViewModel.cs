using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [EmailAddress(ErrorMessage = "Введенные данные не являются електронной почтой")]
        [Display(Name = "Email")]
        [MaxLength(100, ErrorMessage = "Длина {0} не может быть больше {1} символов")]
        public string Email { get; set; }

        [MaxLength(11, ErrorMessage = "Длина {0} не может быть больше {1} символов")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}