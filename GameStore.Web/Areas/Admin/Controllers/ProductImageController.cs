using AutoMapper;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.Areas.Admin.ViewModels.ProductImageViewModel;
using GameStore.Web.Extensions;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.Web.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService, IProductService productService)
        {
            _productImageService = productImageService;
            _productService = productService;
        }
        public ActionResult Index(int productId, string msg = null)
        {
            ViewData["Message"] = msg;
            var product = _productService.GeProduct(productId);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            var images = _productImageService.GetProductImages(productId);
            var model = images.Select(Mapper.Map<ProductImage, ProductImageViewModel>);
            return View(model);
        }

        [HttpGet]
        public ActionResult AddImages(int productId)
        {
            var product = _productService.GeProduct(productId);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            var model = new AddImageViewModel { ProductId = productId };
            return View(model);
        }

        // POST: Admin/ProductImage/AddImages
        [HttpPost]
        public ActionResult AddImages(AddImageViewModel model)
        {
            var images = model.PostedFiles.Select(x => x.ToByteArray());
            foreach (var image in images)
            {
                var productImage = new ProductImage
                {
                    Image = image,
                    ProductId = model.ProductId
                };
                _productImageService.AddImage(productImage);
                Logger.Log.Info($"{User.Identity.Name} добавил изображение для товара {model.ProductId}");
            }

            return RedirectToAction("Index", new { productId = model.ProductId });
        }

        // POST: Admin/ProductImage/Delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var productImage = _productImageService.GetProductImage(id);
            if (productImage == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            var errors = _productImageService.CanDeleteProductImage(productImage);
            if (errors.Count() != 0)
            {
                var msg = errors.Aggregate(string.Empty, (current, variable) => current + variable);
                return RedirectToAction(
                    "Index",
                    new { productId = productImage.ProductId, msg });
            }

            _productImageService.DeleteImage(productImage);
            Logger.Log.Info($"{User.Identity.Name} удалил изображение №{id} для товара №{productImage.ProductId}");
            return RedirectToAction("Index", new { productId = productImage.ProductId });
        }
    }
}