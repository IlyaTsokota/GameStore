using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.Areas.Admin.ViewModels.OrderViewModels
{
    public class DetailsOrderViewModel : OrderViewModel
    {
        [Display(Name = "Сотрудник")]
        public string Manager { get; set; }
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Дата подтверждения заказа")]
        public DateTime AcceptedDate { get; set; }
        [Display(Name = "Коментарий")]
        public string Comment { get; set; }
        public List<OrderDetailsViewModel> OrderDetails { get; set; }
    }
}