using AutoMapper;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.Areas.Admin.ViewModels.ProductViewModel;
using GameStore.Web.Extensions;
using GameStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private const int _pageSize = 10;

        public ProductController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }
        // GET: Admin/Product

        public ActionResult Index(int page = 1)
        {
            var model = GetProductListForModel(page);
            return View(model);
        }

        public ActionResult GetProducts(int page = 1, bool includeDeleted = false, string search = null, int categoryId = 0)
        {
            var model = GetProductListForModel(page, includeDeleted, search,categoryId);
            return PartialView("_Products", model);
        }

        public IndexProductViewModel GetProductListForModel(int page, bool includeDeleted = false, string search = null, int categoryId = 0)
        {
            var categories = _categoryService.GetCategories().Select(x => new SelectListItem { Text = x.Name, Value = x.CategoryId.ToString() });
            var products = _productService.GetProductsForAdmin(includeDeleted, search, categoryId);
            var mappingProducts = products.Select(Mapper.Map<Product, ProductViewModel>).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            var pager = new Pager(page, products.Count(),_pageSize);
            var model = new IndexProductViewModel {Products = mappingProducts, Categories = categories.ToList(), Pager = pager };
            return model;
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = _categoryService.GetCategories()
                .Select(x => new SelectListItem { Text = x.Name, Value = x.CategoryId.ToString() });
            var model = new CreateProductViewModel
            {
                Categories = categories.ToList()
            };

            return View(model);
        }

        // POST: Admin/Product/Create
        [HttpPost]
        public ActionResult Create(CreateProductViewModel model)
        {
            var product = Mapper.Map<CreateProductViewModel, Product>(model);
            var errors = _productService.CanAddProduct(product);
            ModelState.AddModelErrors(errors);
            if (!ModelState.IsValid)
            {
                model.Categories = _categoryService.GetCategories().Select(x => new SelectListItem { Text = x.Name, Value = x.CategoryId.ToString() }).ToList();
                return View(model);
            }

            _productService.CreateProduct(product);
            Logger.Log.Info($"{User.Identity.Name} создал товар №{model.ProductId} {model.Name}");
            return RedirectToAction("Edit", "AttributeValue", new { productId = product.ProductId });
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = _productService.GeProduct(id);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            var model = Mapper.Map<Product, EditProductViewModel>(product);
            return View(model);
        }

        // POST: Admin/Product/Edit
        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var product = Mapper.Map<EditProductViewModel, Product>(model);
            var errors = _productService.CanAddProduct(product).ToList();
            ModelState.AddModelErrors(errors);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _productService.UpdateProduct(product);
            Logger.Log.Info($"{User.Identity.Name} изменил товар {product.Name} №{product.ProductId}");
            return RedirectToAction("Index");
        }

        // POST: Admin/Product/Delete
        
        public ActionResult Delete(int id)
        {
            var product = _productService.GeProduct(id);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            _productService.DeleteProduct(product);
            Logger.Log.Info($"{User.Identity.Name} удалил товар №{id} {product.Name}");
            return RedirectToAction("Index", "Product");
        }

        // POST: Admin/Product/Restore
        
        public ActionResult Restore(int id)
        {
            var product = _productService.GeProduct(id);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            _productService.RestoreProduct(product);
            Logger.Log.Info($"{User.Identity.Name} восстановил товар №{id} {product.Name}");
            return RedirectToAction("Index", "Product");
        }

    }
}
