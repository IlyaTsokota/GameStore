using GameStore.Web.ViewModels.CartViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.OrderViewModels
{
    public class CreateOrderViewModel : OrderViewModel
    {
        [MaxLength(3000, ErrorMessage= "Вы ввели слишком много символов")]
        [Display(Name = "Коментарий")]
        public string Comment { get; set; }

        public List<CartViewModel> CartItems { get; set; }
    }
}