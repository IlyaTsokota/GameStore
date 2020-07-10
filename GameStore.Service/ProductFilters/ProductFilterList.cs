using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service.ProductFilters
{
    public static class ProductFilterList
    {
        public static List<IProductFilter> BaseFiltersForAdmin()
        {
            var list = new List<IProductFilter> { new ProductDeletedFilter(false) };
            return list;
        }

        public static List<IProductFilter> RequiredFiltersForCustomer()
        {
            var list = new List<IProductFilter> { new ProductDeletedFilter(false) };
            return list;
        }
    }
}
