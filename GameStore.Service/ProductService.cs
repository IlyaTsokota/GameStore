using GameStore.Model;
using System.Collections.Generic;
using System.Linq;
using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;

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

        public void CreateProduct(Product product)
        {
            _productRepository.Add(product);
            _unitOfWork.Commit();
        }

        public void DeleteProduct(Product product)
        {
            _productRepository.Delete(product);
            _unitOfWork.Commit();
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
            _unitOfWork.Commit();
        }
        
        public Product GeProduct(int id) => _productRepository.GetById(id);

        public List<Product> GetProducts() => _productRepository.GetAll().ToList();

      
    }
}
