using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels
{
    public class ContactInfoViewModel
    {
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Имя")]
        [MaxLength(50, ErrorMessage = "Длина {0} не может быть больше {1} символов")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Фамилия")]

        [MaxLength(50, ErrorMessage = "Длина {0} не может быть больше {1} символов")]
        public string MiddleName { get; set; }



        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Отчество")]

        [MaxLength(50, ErrorMessage = "Длина {0} не может быть больше {1} символов")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [RegularExpression(@"^(\+)?(38)?0([- _():=+]?\d){9}$", ErrorMessage = "Проверте правильность введенного номера")]
        [Display(Name = "Номер телефона")]
        [MaxLength(13, ErrorMessage = "Длина {0} не может быть больше {1} символов")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Электронный адрес")]
        [EmailAddress(ErrorMessage = "Введенные данные не являются електронной почтой")]
        [MaxLength(100, ErrorMessage = "Длина {0} не может быть больше {1} символов")]
        public string Email { get; set; }
    }
}