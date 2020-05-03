using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.Areas.Admin.ViewModels.OrderViewModels
{
    public class OrderDetailsViewModel
    {

        public int ProductId { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Цена")]
        public int Price { get; set; }
        [Display(Name = "Количество в корзине")]
        public int QuantityInCart { get; set; }
        [Display(Name = "Количество в наличии")]
        public int QuantityInStock { get; set; }
    }
}