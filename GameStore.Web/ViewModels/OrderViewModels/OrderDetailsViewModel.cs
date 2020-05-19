using System.ComponentModel;

namespace GameStore.Web.ViewModels.OrderViewModels
{

    public class OrderDetailsViewModel
    {
        public int ProductId { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Количество")]
        public int QuantityInCart { get; set; }
        [DisplayName("Цена")]
        public int Price { get; set; }

    }
}