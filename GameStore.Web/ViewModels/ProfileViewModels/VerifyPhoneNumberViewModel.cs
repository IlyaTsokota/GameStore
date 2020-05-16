using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.ProfileViewModels
{
    public class VerifyPhoneNumberViewModel
    {
        [Required(ErrorMessage = "Это поле не должно быть пустым")]
        [Display(Name = "Код")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [RegularExpression(@"^(\+)?(38)?0([- _():=+]?\d){9}$", ErrorMessage = "Проверте правильность введенного номера")]
        [Display(Name = "Номер телефона")]
        [MaxLength(13, ErrorMessage = "Длина {0} не может быть больше {1} символов")]
        public string PhoneNumber { get; set; }

    }
}