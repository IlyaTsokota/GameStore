using GameStore.Model;

using System.Collections.Generic;


namespace GameStore.Service.ProductFilters
{
    public interface IProductFilter
    {
        IEnumerable<Product> GetEntities(IEnumerable<Product> products);
    }
}
