using System;
using System.ComponentModel;
using GameStore.Web.Areas.Admin.ViewModels.SupplierViewModels;
namespace GameStore.Web.Areas.Admin.ViewModels.SupplyViewModels
{
    public class SupplyViewModel
    {
        public int SupplyId { get; set; }

        [DisplayName("Дата поставки")]
        public DateTime SupplyDate { get; set; }

        public SupplierViewModel Supplier { get; set; }
    }
}