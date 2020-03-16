using System;

using System.ComponentModel.DataAnnotations;

namespace GameStore.Model
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Range(1, 10)]
        public int Rating { get; set; }

        [MaxLength(5000)]
        public string Text { get; set; }

        public DateTime ReviewDate { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }
    }
}
