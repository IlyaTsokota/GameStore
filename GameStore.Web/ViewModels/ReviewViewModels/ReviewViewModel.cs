using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.ReviewViewModels
{
    public class ReviewViewModel
    {
        public int ReviewId { get; set; }

        public string UserId { get; set; }
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 10)]
        public int Rating { get; set; } = 10;

        [MaxLength(5000)]
        public string Text { get; set; }

        public DateTime ReviewDate { get; set; }

        public string FirstName { get; set; }


    }
}