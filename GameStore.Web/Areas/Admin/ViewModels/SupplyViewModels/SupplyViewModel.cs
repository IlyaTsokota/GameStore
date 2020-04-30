using System;
using System.ComponentModel;

namespace GameStore.Web.Areas.Admin.ViewModels.SupplyViewModels
{
    public class SupplyViewModel
    {
        public int SupplyId { get; set; }

        public int SupplierId { get; set; }

        [DisplayName("Дата поставки")]
        public DateTime SupplyDate { get; set; }
    }
}