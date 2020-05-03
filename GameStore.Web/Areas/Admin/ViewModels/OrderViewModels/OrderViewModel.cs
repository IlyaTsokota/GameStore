using GameStore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.Areas.Admin.ViewModels.OrderViewModels
{
    public class OrderViewModel
    {

        public int OrderId { get; set; }

        public string CustomerId { get; set; }

        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Display(Name = "Номер телефона")]
        public string Phone { get; set; }

        [Display(Name = "Статус")]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = "Дата заказа")]
        public DateTime OrderDate { get; set; }
    }
}