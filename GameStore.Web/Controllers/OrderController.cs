using AutoMapper;
using GameStore.Data.Identity;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.ViewModels;
using GameStore.Web.ViewModels.CartViewModels;
using GameStore.Web.ViewModels.LiqPay;
using GameStore.Web.ViewModels.OrderViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;
        public OrdersController(IOrderService orderService, ICartService cartService, ApplicationUserManager userManager, IProductService productService, ApplicationRoleManager roleManager)
        {
            _orderService = orderService;
            _cartService = cartService;
            _userManager = userManager;
            _productService = productService;
            _roleManager = roleManager;
        }
        public ActionResult Index()
        {
            var customerId = User.Identity.GetUserId();
            var orders = _orderService.GetOrders(customerId: customerId);
            var model = PagerViewModelHelper<OrderViewModel>.ToPagerViewModel(orders);
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var order = _orderService.GetOrder((int)id);
            if (order == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            if (order.CustomerId != User.Identity.GetUserId())
            {
                return RedirectToAction("Forbidden", "Error");
            }

            var model = Mapper.Map<Order, DetailsOrderViewModel>(order);

            if (order.OrderStatusId == 2)
            {
                var urlReferrer = ControllerContext.RequestContext.HttpContext.Request.UrlReferrer.Authority;
                var urlAction = new UrlHelper(ControllerContext.RequestContext).Action("PayResult");
                var returnUrl = urlReferrer + urlAction;
                var liqPayModel = LiqPayHelper.GetLiqPayModel(order, returnUrl);
                model.LiqPayCheckoutFormModel = liqPayModel;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var userId = User.Identity.GetUserId();
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            var model = Mapper.Map<User, CreateOrderViewModel>(user);
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = User.Identity.GetUserId();
            var cartItems = _cartService.GetCartLines(userId);
            var order = Mapper.Map<CreateOrderViewModel, Order>(model);
            order.OrderDate = DateTime.Now;
            order.AcceptedDate = DateTime.Now;
            order.OrderStatusId = 1;
            order.CustomerId = userId;
            var orderedProducts = cartItems.Where(x => x.Quantity != 0)
                .Select(x => new { product = _productService.GeProduct(x.ProductId), x.Quantity });
            var orderDetails = orderedProducts.Select(x => new OrderDetails
            {
                ProductId = x.product.ProductId,
                Price = x.product.Price,
                Quantity = x.Quantity
            }).ToList();
            order.OrderDetails = orderDetails;
            _orderService.CreateOrder(order);
            _cartService.Clear(userId);

            await _userManager.SendEmailAsync(userId, "Заказ принят в обработку", $"Ваш заказ был принят в обработку, ожидайте пока с вами свяжется менеджер!").ConfigureAwait(false);

            var role = await _roleManager.FindByNameAsync("Manager").ConfigureAwait(false);
            var managersId = role.Users.Select(x => x.UserId);
            var managers = managersId.Select(x => _userManager.FindById(x)).ToList();
            var callbackUrl = Url.Action("Details", "Order", new { area = "Admin", id = order.OrderId }, protocol: Request.Url.Scheme);
            foreach (var item in managers)
            {
                await _userManager.SendEmailAsync(item.Id, $"На сайте появился новый заказ № {order.OrderId}", $"Перейдите для принятия этого заказа  <a href=\"" + callbackUrl + "\">здесь</a>").ConfigureAwait(false);
            }
            Logger.Log.Info($"На сайте появился заказ №{order.OrderId}");
            return RedirectToAction("Details", new { id = order.OrderId });
        }

        [HttpPost]
        public async Task<ActionResult> PayResult()
        {
            var requestDictionary = Request.Form.AllKeys.ToDictionary(key => key, key => Request.Form[key]);

            var requestData = Convert.FromBase64String(requestDictionary["data"]);
            var decodedString = Encoding.UTF8.GetString(requestData);
            var requestDataDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedString);

            var mySignature = LiqPayHelper.GetLiqPaySignature(requestDictionary["data"]);

            if (mySignature != requestDictionary["signature"])
            {
                return View("Error");
            }

            if (requestDataDictionary["status"] != "sandbox" && requestDataDictionary["status"] != "success")
            {
                return View("PayResultFailure");
            }

            var orderId = Convert.ToInt32(requestDataDictionary["order_id"]);
            var order = _orderService.GetOrder(orderId);
            if (order == null)
            {
                Logger.Log.Warn($"Заказ №{orderId} оплачен, но не был найден!");
                return View("Error");
            }

            _orderService.OrderPaid(order);
            Logger.Log.Info($"{User.Identity.Name} оплатил заказ №{orderId} через LiqPay");

            var userId = User.Identity.GetUserId();
            await _userManager.SendEmailAsync(userId, "Вы оплатили заказ", $"Ваш заказ №{order.OrderId} был оплачен, ждите пока вам вышлют товар").ConfigureAwait(false);

            var role = await _roleManager.FindByNameAsync("Manager").ConfigureAwait(false);
            var managersId = role.Users.Select(x => x.UserId);
            var managers = managersId.Select(x => _userManager.FindById(x)).ToList();
            var callbackUrl = Url.Action("Details", "Order", new { area = "Admin", id = order.OrderId }, protocol: Request.Url.Scheme);
            foreach (var item in managers)
            {
                await _userManager.SendEmailAsync(item.Id, $"Заказ № {order.OrderId} был оплачен и ожидает отправки", $"Перейдите для отправки этого заказа  <a href=\"" + callbackUrl + "\">здесь</a>").ConfigureAwait(false);
            }
            return View("PayResultSuccess");
        }

    }
}