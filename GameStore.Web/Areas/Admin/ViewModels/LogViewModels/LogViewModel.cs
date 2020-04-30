using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.Areas.Admin.ViewModels.LogViewModels
{
    public class LogViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Дата перехвата")]
        public DateTime Date { get; set; }

        [Display(Name = "Уровень")]
        public string Level { get; set; }


        [Display(Name = "Сообщение")]
        public string Message { get; set; }
    }
}