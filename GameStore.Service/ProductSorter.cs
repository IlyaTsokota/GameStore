using GameStore.Model;
using GameStore.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public interface IProductSorter
    {
        IEnumerable<Product> Sort(IEnumerable<Product> products);
    }

    public class ProductSorter : IProductSorter
    {
        private readonly ProductOrderBy _orderBy = ProductOrderBy.NewDesc;

        public ProductSorter()
        {
        }

        public ProductSorter(ProductOrderBy orderBy)
        {
            _orderBy = orderBy;
        }

        public IEnumerable<Product> Sort(IEnumerable<Product> products)
        {
            switch (_orderBy)
            {
                case ProductOrderBy.NewDesc:
                    products = products.OrderByDescending(x => x.ProductId);
                    break;
                case ProductOrderBy.PopularityDesc:
                    products = products.OrderByDescending(x => x.OrderDetails.Sum(orderDetails => orderDetails.Quantity));
                    break;
                case ProductOrderBy.RatingDesc:
                    products = products.OrderByDescending(x => x.Reviews.Any() ? x.Reviews.Average(review => review.Rating) : 0);
                    break;
                case ProductOrderBy.NameAsc:
                    products = products.OrderBy(x => x.Name);
                    break;
                case ProductOrderBy.NameDesc:
                    products = products.OrderByDescending(x => x.Name);
                    break;
                case ProductOrderBy.PriceAsc:
                    products = products.OrderBy(x => x.Price);
                    break;
                case ProductOrderBy.PriceDesc:
                    products = products.OrderByDescending(x => x.Price);
                    break;
                default:
                    products = products.OrderByDescending(x => x.Name);
                    break;
            }

            return products;
        }
    }
}
