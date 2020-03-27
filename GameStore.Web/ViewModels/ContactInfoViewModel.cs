using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels
{
    public class ContactInfoViewModel
    {
        [Required]
         [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string MiddleName { get; set; }


        [Required]
        [Display(Name = "Отчество")]
        public string LastName { get; set; }

        [Required]
         [RegularExpression(@"^(\+)?(38)?0([- _():=+]?\d){9}$")]

        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Электронный адрес")]
        [EmailAddress]
         public string Email { get; set; }
    }
}