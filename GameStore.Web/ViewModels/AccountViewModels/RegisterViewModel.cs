using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.AccountViewModel
{
    public class RegisterViewModel : ContactInfoViewModel
    {
        

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}