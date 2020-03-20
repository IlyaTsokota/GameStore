
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.ViewModels
{
    public class ContactInfoViewModel
    {
        [Required]
        [RegularExpression(@"^[А-Яа-яЁёЇїІіЄєҐґA-Za-z\-']+$", ErrorMessage = "Ошибка ввода")]
        [MaxLength(50,ErrorMessage = "Поле не должно содержать больше 50 символов!")]
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[А-Яа-яЁёЇїІіЄєҐґA-Za-z\-']+$", ErrorMessage = "Ошибка ввода")]
        [MaxLength(50, ErrorMessage = "Поле не должно содержать больше 50 символов!")]
        [DisplayName("Фамилия")]
        public string MiddleName { get; set; }

        [RegularExpression(@"^[А-Яа-яЁёЇїІіЄєҐґA-Za-z\-']+$", ErrorMessage = "Ошибка ввода")]
        [MaxLength(50, ErrorMessage = "Поле не должно содержать больше 50 символов!")]
        [DisplayName("Отчество")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(13, ErrorMessage = "Поле не должно содержать больше 50 символов!")]
        [RegularExpression(@"^(\+)?(38)?0([- _():=+]?\d){9}$", ErrorMessage = "Вы ввели номер в неправельном формате!")]
        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Поле не должно содержать больше 100 символов!")]
        [EmailAddress]
        [Display(Name = "Почтовый адрес")]
        public string Email { get; set; }
    }
}