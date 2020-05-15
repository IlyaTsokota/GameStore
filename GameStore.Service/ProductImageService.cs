using GameStore.Data.Infrastructure;
using GameStore.Data.Repositories;
using GameStore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Service
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ProductImageService(IUnitOfWork unitOfWork, IProductImageRepository productImageRepository)
        {
            _unitOfWork = unitOfWork;
            _productImageRepository = productImageRepository;
        }

        public ProductImage GetProductImage(int id)
        {
            var productImage = _productImageRepository.GetById(id);
            return productImage;
        }

        public List<ProductImage> GetProductImages(int productId)
        {
            var productImages = _productImageRepository.GetMany(x => x.ProductId == productId);
            return productImages.ToList();
        }

        public void AddImage(ProductImage productImage)
        {
            _productImageRepository.Add(productImage);
            _unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanDeleteProductImage(ProductImage image)
        {
            var productImages = _productImageRepository.GetMany(x => x.ProductId == image.ProductId);
            if (productImages.Count() == 1)
            {
                yield return new ValidationResult("Нельзя удалить последнее изображение!");
            }
        }

        public void DeleteImage(ProductImage image)
        {
            _productImageRepository.Delete(image);
            _unitOfWork.Commit();
        }
    }
}
