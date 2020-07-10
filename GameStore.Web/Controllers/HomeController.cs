using AutoMapper;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.ViewModels.HomeViewModels;
using GameStore.Web.ViewModels.ProductViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [Authorize]
        public ActionResult Index()
        {
            var products = _productService.GetProducts().Select(Mapper.Map<Product, ProductViewModel>);
            var playstationList = products.Where(x => x.Category.Name.Contains("Playstation")).Take(10).ToList();
            var xboxList = products.Where(x => x.Category.Name.Contains("Xbox")).Take(10).ToList();
            var allProducts = products.Take(10).ToList();
            var model = new HomeIndexViewModel
            {
                PlaystationList = playstationList,
                XboxList = xboxList,
                AllProducts = allProducts
            };
            return View(model);
        }

        public ActionResult GetCategories(string name)
        {
            var categories = _categoryService.GetCategories();
            categories = categories.Where(x => x.Name.Contains(name)).ToList();
            var model = categories.Select(Mapper.Map<Category, CategoryViewModel>).ToList();
            return PartialView("_Categories", model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Warranty()
        {
            return View();
        }
        public ActionResult PaymentDeliveryInfo()
        {
            return View();
        }
    }
}