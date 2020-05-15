using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.Areas.Admin.ViewModels.ProductViewModel
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [MaxLength(200)]
        [Display(Name="Название")]
        public string Name { get; set; }


        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        [Display(Name = "Старая цена")]
        [Range(0, int.MaxValue)]
        public int OldPrice { get; set; }

       
        [Range(0, int.MaxValue)]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }


    }
}