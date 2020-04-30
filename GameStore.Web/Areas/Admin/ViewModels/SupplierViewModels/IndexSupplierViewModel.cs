using GameStore.Web.Models;
using System.Collections.Generic;


namespace GameStore.Web.Areas.Admin.ViewModels.SupplierViewModels
{
    public class IndexSupplierViewModel
    {
        public List<SupplierViewModel> Suppliers { get; set; }

        public Pager Pager { get; set; }
    }
}