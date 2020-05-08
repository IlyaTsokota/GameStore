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
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        public ActionResult Index()
        {
            var products = _productService.GetProducts().Select(Mapper.Map<Product, ProductViewModel>);
            var playstationList = products.Where(x=>x.Category.Name.Contains("Playstation")).Take(10).ToList();
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}