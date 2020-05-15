using GameStore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public interface IProductImageService
    {
        ProductImage GetProductImage(int id);

        List<ProductImage> GetProductImages(int productId);

        void AddImage(ProductImage productImage);

        IEnumerable<ValidationResult> CanDeleteProductImage(ProductImage image);

        void DeleteImage(ProductImage image);
    }
}
