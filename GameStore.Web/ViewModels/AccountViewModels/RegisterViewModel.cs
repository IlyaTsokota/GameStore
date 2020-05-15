using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.AccountViewModels
{
    public class RegisterViewModel : ContactInfoViewModel
    {

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        [MaxLength(11, ErrorMessage = "Длина {0} не может быть больше {1} символов")]
        public string Password { get; set; }

        [MaxLength(11, ErrorMessage = "Длина {0} не может быть больше {1} символов")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [DisplayName("Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}