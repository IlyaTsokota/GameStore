
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;


namespace GameStore.Model
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public List<Order> Orders { get; set; }
    }
}
