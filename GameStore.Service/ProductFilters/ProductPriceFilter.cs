using GameStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.ProductFilters
{
    public class ProductPriceFilter : IProductFilter
    {
        private readonly int _minPrice;

        private readonly int _maxPrice;

        public ProductPriceFilter(int minPrice, int maxPrice)
        {
            _minPrice = minPrice;
            _maxPrice = maxPrice;
        }

        public IEnumerable<Product> GetEntities(IEnumerable<Product> products)
        {

            products = products?.Where(x => x.Price >= _minPrice && x.Price <= _maxPrice);
            return products;
        }
    }
}
