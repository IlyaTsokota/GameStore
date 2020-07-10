using AutoMapper;
using GameStore.Model;
using GameStore.Service;
using GameStore.Service.Enums;
using GameStore.Service.ProductFilters;
using GameStore.Web.Helpers;
using GameStore.Web.Models;
using GameStore.Web.ViewModels;
using GameStore.Web.ViewModels.ProductViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IAttributeService _attributeService;
        private readonly ICategoryService _categoryService;
        private const int _pageSize = 12;

        public ProductsController(ICategoryService categoryService, IProductService productService, IAttributeService attributeService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _attributeService = attributeService;
        }
        // GET: Product

        public ActionResult Products(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            var category = _categoryService.GetCategory((int)id);
            if (category == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            var attributes = _attributeService.GetAttributesForFiltering(category.CategoryId);
            var filters = new List<IProductFilter> { new ProductCategoryFilter(category.CategoryId) };
            var sorter = new ProductSorter();
            var products = _productService.GetProductsForCustomer(sorter, filters);
            var productViewModels = PagerViewModelHelper<ProductViewModel>.ToPagerViewModel(products, 1, _pageSize);
            var minPrice = products.Any() ? products.Min(x => x.Price) : 0;
            var maxPrice = products.Any() ? products.Max(x => x.Price) : 0;
            var productFilterViewModel = new ProductFilterViewModel
            {
                CategoryId = category.CategoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };
            var model = new CategoryProductsViewModel
            {
                Attributes = attributes,
                Name = category.Name,
                Products = productViewModels,
                ProductFilterViewModel = productFilterViewModel
            };
            return View("Index", model);
        }
        public ActionResult GetProducts(CategoryProductsViewModel model)
        {
            var filterModel = model.ProductFilterViewModel;
            var filters = ProductFilterHelper.GetFilters(filterModel);
            var sorter = new ProductSorter((ProductOrderBy)filterModel.OrderBy);
            var products = _productService.GetProductsForCustomer(sorter, filters);
            var productViewModels = PagerViewModelHelper<ProductViewModel>.ToPagerViewModel(products, model.Page, _pageSize);
            return PartialView("_Products", productViewModels);
        }

       

        [HttpGet]
        public ActionResult Details(int id)
        {
            var product = _productService.GeProduct(id);
            var model = Mapper.Map<Product, DetailsProductViewModel>(product);
            model.CanAddReview = product.Reviews.All(x => x.UserId != User.Identity.GetUserId());
            return View(model);
        }
    }
}