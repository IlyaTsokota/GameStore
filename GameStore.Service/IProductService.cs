
using System.Collections.Generic;

using GameStore.Model;

namespace GameStore.Service
{
    public interface IProductService
    {
        Product GeProduct(int id);

        List<Product> GetProducts();

        void CreateProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(Product product);

    }
}
