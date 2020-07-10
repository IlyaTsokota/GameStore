using GameStore.Model;
using System.Collections.Generic;
using System.Linq;
using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using System.ComponentModel.DataAnnotations;
using GameStore.Service.ProductFilters;

namespace GameStore.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<ValidationResult> CanAddProduct(Product newProduct)
        {
            Product product;
            if (newProduct.ProductId == 0)
            {
                product = _productRepository.Get(x => x.Name == newProduct.Name);
            }
            else
            {
                product = _productRepository.Get(
                   (x => x.Name == newProduct.Name && x.ProductId != newProduct.ProductId));
            }
            if (product != null)
            {
                yield return new ValidationResult("Товар с данным названием уже существует!");
            }
            if (newProduct.OldPrice != 0 && newProduct.OldPrice <= newProduct.Price)
            {
                yield return new ValidationResult("Старая цена не может быть меньше новой!");
            }
        }

        public void CreateProduct(Product product)
        {
            _productRepository.Add(product);
            _unitOfWork.Commit();
        }

        public void DeleteProduct(Product product)
        {
            product.IsDeleted = true;
            _productRepository.Update(product);
            _unitOfWork.Commit();
        }

        public void RestoreProduct(Product product)
        {
            product.IsDeleted = false;
            _productRepository.Update(product);
            _unitOfWork.Commit();
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
            _unitOfWork.Commit();
        }
        public List<Product> GetProductsForCustomer(IProductSorter productSorter, List<IProductFilter> productFilters = null)
        {
            productFilters = productFilters == null
                                 ? ProductFilterList.RequiredFiltersForCustomer()
                                 : productFilters.Union(ProductFilterList.RequiredFiltersForCustomer()).ToList();

            var products = ApplyFilters(productFilters, productSorter);
            return SortByAvailability(products);
        }

        public List<Product> GetProductsForAdmin(
            IProductSorter productSorter,
            List<IProductFilter> productFilters = null)
        {
            if (productFilters == null)
            {
                productFilters = ProductFilterList.BaseFiltersForAdmin();
            }

            var products = ApplyFilters(productFilters, productSorter);
            return products.ToList();
        }

        public Product GeProduct(int id) => _productRepository.GetById(id);

        public List<Product> GetProducts() => _productRepository.GetAll().ToList();

        public List<Product> GetProductsForAdmin(bool includeDeleted = false, string search = null, int categoryId = 0)
        {
            var products = includeDeleted ? _productRepository.GetMany(x => x.IsDeleted) : _productRepository.GetMany(x => !x.IsDeleted);
            if (!string.IsNullOrEmpty(search))
                products = products.Where(x => x.Name.Contains(search));
            if (categoryId != 0)
                products = products.Where(x => x.CategoryId == categoryId);
            return products.ToList();
        }

        public List<Product> GetProductsForCustomer(string search = null, string category = null)
        {
            var products = _productRepository.GetMany(x => !x.IsDeleted);
            if (!string.IsNullOrEmpty(search))
                products = products.Where(x => x.Name.Contains(search));
            if (!string.IsNullOrEmpty(category))
                products = products.Where(x => x.Category.Name == category);
            return products.ToList();
        }

        private static List<Product> SortByAvailability(IEnumerable<Product> products)
        {
            return products.OrderByDescending(x => x.Quantity > 0 ? 1 : 0).ToList();
        }

        private IEnumerable<Product> ApplyFilters(IEnumerable<IProductFilter> productFilters, IProductSorter productSorter)
        {
            var products = _productRepository.GetAll();
            products = productFilters.Aggregate(products, (current, productFilter) => productFilter.GetEntities(current));
            var sortedProducts = productSorter.Sort(products);
            return sortedProducts;
        }

    }
}
