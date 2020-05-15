using AutoMapper;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.Models;
using GameStore.Web.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private const int _pageSize = 10;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: Product
        public ActionResult Index(int page = 1, string search = null, string category = null)
        {
            var model = GetListProducts(page, search, category);
            return View(model);
        }

        public ActionResult GetProducts(int page = 1, string search = null, string category = null)
        {
            var model = GetListProducts(page, search, category);
            return PartialView("_Products", model);
        }

        public IndexProductViewModel GetListProducts(int page, string search, string category)
        {
            var products = _productService.GetProductsForCustomer(search, category);
            var mappingProduct = products.Select(Mapper.Map<Product, ProductViewModel>).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            var pager = new Pager(page, products.Count(), _pageSize);
            var model = new IndexProductViewModel { Products = mappingProduct, Pager = pager };
            return model;
        }

        public ActionResult Details(int id)
        {

            return View();
        }
    }
}