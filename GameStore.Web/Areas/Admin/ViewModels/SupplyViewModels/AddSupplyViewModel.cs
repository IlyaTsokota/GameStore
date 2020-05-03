using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Areas.Admin.ViewModels.SupplyViewModels
{
    public class AddSupplyViewModel : SupplyViewModel
    {
        [Required]
        public int SupplierId { get; set; }

        public List<SupplyProductViewModel> SupplyProducts { get; set; }

        public List<SelectListItem> Suppliers { get; set; }

        public List<SelectListItem> Products { get; set; }
    }
}