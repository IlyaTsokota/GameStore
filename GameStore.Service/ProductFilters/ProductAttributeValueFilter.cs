
using GameStore.Model;
using System.Collections.Generic;
using System.Linq;


namespace GameStore.Service.ProductFilters
{
    public class ProductAttributeValueFilter : IProductFilter
    {
        private readonly IEnumerable<AttributeValue> _selectedAttributeValues;

        public ProductAttributeValueFilter(IEnumerable<AttributeValue> selectedAttributeValues)
        {
            _selectedAttributeValues = selectedAttributeValues;
        }

        public IEnumerable<Product> GetEntities(IEnumerable<Product> products)
        {
            var groupedAttributeValues = _selectedAttributeValues.GroupBy(x => x.AttributeId)
                .Where(x => x.Any(z => z.Value != null));

            // Get products where all attributes have any selected value equals value of attribute of product
            products = products.Where(
                product => groupedAttributeValues.All(
                    attributeValue => product.AttributeValues.Any(
                        productAttributeValue => productAttributeValue.AttributeId == attributeValue.Key
                                                 && _selectedAttributeValues.Any(
                                                     av => av.Value == productAttributeValue.Value))));
            return products;
        }
    }
}
