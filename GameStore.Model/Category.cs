
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;


namespace GameStore.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public byte[] Image { get; set; }

        public virtual List<Attribute> Attributes { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
