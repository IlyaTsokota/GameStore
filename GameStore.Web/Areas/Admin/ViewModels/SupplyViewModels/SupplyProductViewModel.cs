using System.ComponentModel.DataAnnotations;
using GameStore.Web.Areas.Admin.ViewModels.ProductViewModel;
namespace GameStore.Web.Areas.Admin.ViewModels.SupplyViewModels
{
    public class SupplyProductViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        [Display(Name = "Цена")]
        public int Price { get; set; }

    }
}