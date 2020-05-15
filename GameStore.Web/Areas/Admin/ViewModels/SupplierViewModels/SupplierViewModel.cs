
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace GameStore.Web.Areas.Admin.ViewModels.SupplierViewModels
{
    public class SupplierViewModel
    {
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым.")]
        [MaxLength(50, ErrorMessage = "Поле {0} не может содержать больше {1} символов.")]
        [Display(Name = "Поставщик")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым.")]
        [MaxLength(13)]
        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

    }
}