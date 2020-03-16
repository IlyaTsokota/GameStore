
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;


namespace GameStore.Model
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(13)]
        public string Phone { get; set; }

        public List<Supply> Supplies { get; set; }

        public bool IsDeleted { get; set; }
    }
}
