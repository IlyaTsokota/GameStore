using GameStore.Model;
using System.Collections.Generic;
using System.Linq;
using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using System.ComponentModel.DataAnnotations;

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
                yield return new ValidationResult("Товар с данным названием уже существует!!");
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
        
        public Product GeProduct(int id) => _productRepository.GetById(id);

        public List<Product> GetProducts() => _productRepository.GetAll().ToList();

        public List<Product> GetProductsForAdmin(bool includeDeleted = false, string search = null, int categoryId = 0)
        {
            var products = includeDeleted ? _productRepository.GetMany(x=>x.IsDeleted) : _productRepository.GetMany(x => !x.IsDeleted);
            if (!string.IsNullOrEmpty(search))
                products = products.Where(x => x.Name.Contains(search));
            if (categoryId != 0)
                products = products.Where(x => x.CategoryId == categoryId);
            return products.ToList();
        }
      
    }
}
