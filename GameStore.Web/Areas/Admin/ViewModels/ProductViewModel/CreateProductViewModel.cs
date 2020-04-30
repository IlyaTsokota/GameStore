using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Areas.Admin.ViewModels.ProductViewModel
{
    public class CreateProductViewModel : ProductViewModel
    {
        [AllowHtml]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        public string Description { get; set; }

        [Display(Name = "Гарантия")]
        [Required(ErrorMessage= "Поле не должно быть пустым")]
        public string Warranty { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage ="Выберите категорию")]
        public int CategoryId { get; set; }
     
        [Required(ErrorMessage ="Вы должны выбрать изображение")]
        public List<HttpPostedFileBase> Images { get; set; }

        public List<SelectListItem> Categories { get; set; }
    }
}