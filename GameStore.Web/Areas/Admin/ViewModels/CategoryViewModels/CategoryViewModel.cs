
using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Areas.Admin.ViewModels.CategoryViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
       
        [Required(ErrorMessage ="Поле не может быть пустым!")]
        [StringLength(50,MinimumLength =2,ErrorMessage = "Значение {0} не может превышать {1} символов и быть меньше чем {2} символа.")]
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Изображение")]
        public byte[] Image { get; set; }

    }
}