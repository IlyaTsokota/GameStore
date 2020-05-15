
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;


namespace GameStore.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }
        [Range(0, int.MaxValue)]
        public int OldPrice { get; set; }
        [Required]
        [MaxLength(20)]
        public string Warranty { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<AttributeValue> AttributeValues { get; set; }

        public virtual List<ProductImage> Images { get; set; }

        public virtual List<Review> Reviews { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }

        public virtual List<SupplyProduct> SupplyProducts { get; set; }
    }
}
