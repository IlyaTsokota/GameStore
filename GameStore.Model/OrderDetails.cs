
using System.ComponentModel.DataAnnotations;


namespace GameStore.Model
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Price { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
