using AutoMapper;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.ViewModels.CartViewModels;
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
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        private readonly IProductService _productService;


        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = GetListItems();
            return View(model);
        }

        public IndexCartViewModel GetListItems()
        {
            var userId = User.Identity.GetUserId();
            var cartLines = _cartService.GetCartLines(userId);
            var totalValue = _cartService.GetTotalValue();
            var cartItems = cartLines.Select(Mapper.Map<CartLine, CartViewModel>).ToList();
            var model = new IndexCartViewModel
            {
                CartItems = cartItems,
                TotalValue = totalValue
            };
            return model;
        }

        public ActionResult AddToCart(int productId)
        {
            var product = _productService.GeProduct(productId);
            var model = Mapper.Map<Product, ProductViewModel>(product);
            var userId = User.Identity.GetUserId();
            if (product != null)
            {
                _cartService.AddItem(product, 1, userId);
            }
            else
            {
                return RedirectToAction("NotFound", "Error");
            }
            return PartialView("_AddNotification", model);
        }

        public ActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveLine(id);
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Clear()
        {
            var userId = User.Identity.GetUserId();
            _cartService.Clear(userId);
            return RedirectToAction("Index", "Cart");
        }
        public JsonResult QuanityChange(int type, int pId)
        {

            int quantity;
            var cartLine = _cartService.Get(pId);
            if (cartLine == null)
            {
                return Json(new { d = "0" });
            }
            // if type 0, decrease quantity
            // if type 1, increase quanity
            switch (type)
            {
                case 0:
                    cartLine.Quantity--;
                    break;
                case 1:
                    cartLine.Quantity++;
                    break;
                case -1:
                    cartLine.Quantity = 0;
                    break;
                default:
                    return Json(new { d = "0" });
            }

            if (cartLine.Quantity == 0)
            {
                _cartService.RemoveLine(pId);
                quantity = 0;
            }
            else
            {
                _cartService.Update(cartLine);
                quantity = cartLine.Quantity;
            }
            return Json(new { d = quantity });
        }

        [HttpGet]
        public JsonResult UpdateTotal()
        {

            double total;
            try
            {
                total = _cartService.GetTotalValue();
            }
            catch (Exception)
            {
                total = 0;
            }

            return Json(new { d = total.ToString("# грн") }, JsonRequestBehavior.AllowGet);
        }
    }
}