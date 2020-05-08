using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.ViewModels.ProductViewModels
{
    public class ProductImageViewModel
    {
        public int ProductImageId { get; set; }

    
        public int ProductId { get; set; }

     
        public byte[] Image { get; set; }
    }
}