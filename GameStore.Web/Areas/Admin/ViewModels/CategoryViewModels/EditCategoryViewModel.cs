
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace GameStore.Web.Areas.Admin.ViewModels.CategoryViewModels
{
    public class EditCategoryViewModel : CategoryViewModel 
    {
        [Display(Name = "Изображение")]
        public HttpPostedFileBase EditImage { get; set; }
    }
}