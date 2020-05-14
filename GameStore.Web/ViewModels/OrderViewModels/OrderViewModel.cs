using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.OrderViewModels
{
    public class OrderViewModel : ContactInfoViewModel
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Введите адрес")]
        [MaxLength(200, ErrorMessage = "Вы ввели слишком много символов")]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Дата заказа")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Статус заказа")]
        public string OrderStatus { get; set; }
    }
}