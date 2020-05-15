using GameStore.Web.ViewModels.CategoryViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.ProductViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        public int Price { get; set; }

        [Display(Name = "Старая цена")]
        public int OldPrice { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }

        public CategoryViewModel Category { get; set; }

        public List<ProductImageViewModel> Images { get; set; }
    }
}