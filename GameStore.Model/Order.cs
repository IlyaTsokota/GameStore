using System;

using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;


namespace GameStore.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        [Required]
        [Phone]
        [MaxLength(13)]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(3000)]
        public string Comment { get; set; }

        public int OrderStatusId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime AcceptedDate { get; set; }

        [Required]
        public string CustomerId { get; set; }

        public string ManagerId { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }

        public virtual User Customer { get; set; }

        public virtual User Manager { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
