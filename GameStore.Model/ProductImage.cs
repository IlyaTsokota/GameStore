
using System.ComponentModel.DataAnnotations;


namespace GameStore.Model
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public byte[] Image { get; set; }

        public virtual Product Product { get; set; }
    }
}
