using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.ProductViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [Display(Name ="Название")]
        public string Name { get; set; }
    }
}