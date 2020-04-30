using System.ComponentModel.DataAnnotations;
using System.Web;

namespace GameStore.Web.Areas.Admin.ViewModels.CategoryViewModels
{
    public class CreateCategoryViewModel 
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Значение {0} не может превышать {1} символов и быть меньше чем {2} символа.")]
        [Display(Name = "Название")]
        public string Name { get; set; }
        
        [Required(ErrorMessage ="Изображение должно быть выбрано.")]
        [Display(Name = "Изображение")]
        public HttpPostedFileBase CreateImage { get; set; }
    }
}