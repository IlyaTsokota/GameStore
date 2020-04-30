
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Model;

namespace GameStore.Service
{
    public interface IProductService
    {
        Product GeProduct(int id);

        List<Product> GetProducts();

        List<Product> GetProductsForAdmin(bool includeDeleted, string search, int categoryId);

        IEnumerable<ValidationResult> CanAddProduct(Product newProduct);

        void CreateProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(Product product);

        void RestoreProduct(Product product);
    }
}
