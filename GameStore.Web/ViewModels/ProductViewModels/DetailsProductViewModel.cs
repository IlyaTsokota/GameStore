using GameStore.Web.ViewModels.ReviewViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.ProductViewModels
{
    public class DetailsProductViewModel 
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public int OldPrice { get; set; }

        public string Warranty { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public double Rating { get; set; } = 0;

        public int ReviewCount { get; set; }

        public bool CanAddReview { get; set; }

        public Dictionary<string, string> Characteristics { get; set; }

        public List<byte[]> Images { get; set; }

        public ReviewViewModel NewReview { get; set; }
    }
}