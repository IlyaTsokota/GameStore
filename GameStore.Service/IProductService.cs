
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Model;
using GameStore.Service.ProductFilters;

namespace GameStore.Service
{
    public interface IProductService
    {
        Product GeProduct(int id);

        List<Product> GetProducts();

        List<Product> GetProductsForAdmin(bool includeDeleted, string search, int categoryId);

        List<Product> GetProductsForCustomer(string search, string category);

        List<Product> GetProductsForCustomer(
         IProductSorter productSorter,
         List<IProductFilter> productFilters = null);

        List<Product> GetProductsForAdmin(
            IProductSorter productSorter,
            List<IProductFilter> productFilters = null);

        IEnumerable<ValidationResult> CanAddProduct(Product newProduct);

        void CreateProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(Product product);

        void RestoreProduct(Product product);
    }
}
