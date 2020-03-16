
using System.ComponentModel.DataAnnotations;


namespace GameStore.Model
{
    public class SupplyProduct
    {
        public int SupplyProductId { get; set; }
        [Required]
        public int SupplyId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual Supply Supply { get; set; }
    }
}
