using AutoMapper;
using GameStore.Data.Identity;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.ViewModels.CartViewModels;
using GameStore.Web.ViewModels.OrderViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        
        private readonly ICartService _cartService;
        private readonly ApplicationUserManager _userManager;
        public OrderController(IOrderService orderService, ICartService cartService, ApplicationUserManager userManager)
        {
            _orderService = orderService;
            _cartService = cartService;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var userId = User.Identity.GetUserId();
            var cartItems = _cartService.GetCartLines(userId);
            var mappCartItems = cartItems.Select(Mapper.Map<CartLine, CartViewModel>).ToList();
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            var model = Mapper.Map<User, CreateOrderViewModel>(user);
            model.CartItems = mappCartItems;
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateOrderViewModel model)
        {
           
            
            return View();
        }
    }
}