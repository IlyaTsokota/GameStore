using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.AccountViewModels
{
    public class ResetPasswordViewModel
    {

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [EmailAddress(ErrorMessage = "Введенные данные не являються електронной почтой")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [StringLength(100, ErrorMessage = "Длина {0} должна быть не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!!!")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}