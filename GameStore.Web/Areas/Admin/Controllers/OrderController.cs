using AutoMapper;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.Areas.Admin.ViewModels.OrderViewModels;
using GameStore.Web.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        private readonly IOrderStatusService _orderStatusService;

        private const int _pageSize = 10; 
        public OrderController(IOrderService orderService, IOrderStatusService orderStatusService)
        {
            _orderService = orderService;
            _orderStatusService = orderStatusService;
        }

        // GET: Admin/Order
        public ActionResult Index(int page = 1)
        {
            var orderStatuses = _orderStatusService.GetOrderStatuses();
            var statuses = new List<SelectListItem>(orderStatuses.Select(x => new SelectListItem { Text = x.Name, Value = x.OrderStatusId.ToString() }));
            var orders = _orderService.GetOrders();
            var pagerViewModel = orders.Select(Mapper.Map<Order, OrderViewModel>).Skip((page-1)*_pageSize).Take(_pageSize).ToList();
            var pager = new Pager(page, orders.Count(), _pageSize);
            var model = new IndexOrderViewModel { OrderStatuses = statuses, Orders = pagerViewModel, Pager = pager };
            return View(model);
        }

        // GET: Admin/Order/GetOrders
        public ActionResult GetOrders(IndexOrderViewModel model, int page = 1)
        {
            var managerId = model.OnlyMyOrders != null ? User?.Identity.GetUserId() : null;
            var orders = _orderService.GetOrders(model.OrderStatusId, managerId: managerId); 
            var pagerViewModel = orders.Select(Mapper.Map<Order, OrderViewModel>).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            var pager = new Pager(page, orders.Count(), _pageSize);
            var models = new IndexOrderViewModel {Orders = pagerViewModel, Pager = pager };
            return PartialView("_Orders", models);
        }

      
        // GET: Admin/Order/Details
        public ActionResult Details(int id, string message)
        {
            ViewData["Message"] = message;
            var order = _orderService.GetOrder(id);
            if (order == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            var model = Mapper.Map<Order, DetailsOrderViewModel>(order);
            return View(model);
        }

        // POST: Admin/Order/AcceptOrder
        [HttpPost]
        public ActionResult AcceptOrder(int id)
        {
            var order = _orderService.GetOrder(id);
            if (order == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            var errors = _orderService.CanAcceptOrder(order);
            if (errors.Count() != 0)
            {
                var msg = string.Empty;
                errors.ForEach(x => msg += x.ErrorMessage);
                return RedirectToAction("Details", new { id, message = msg });
            }

            var userId = User.Identity.GetUserId();
            _orderService.AcceptOrder(order, userId);
            Logger.Log.Info($"{User.Identity.Name} принял заказ №{order.OrderId}");
            return RedirectToAction("Details", new { id });
        }

        // POST: Admin/Order/OrderPaid
        [HttpPost]
        public ActionResult OrderPaid(int id)
        {
            var order = _orderService.GetOrder(id);
            if (order == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            if (order.ManagerId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Details", new { id, message = "Вы не принимали этот заказ!" });
            }

            _orderService.OrderPaid(order);
            Logger.Log.Info($"{User.Identity.Name} перевел заказ №{order.OrderId} в статус \"Оплачен\"");
            return RedirectToAction("Details", new { id });
        }

        // POST: Admin/Order/OrderCompleted
        [HttpPost]
        public ActionResult OrderCompleted(int id)
        {
            var order = _orderService.GetOrder(id);
            if (order == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            if (order.ManagerId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Details", new { id, message = "Вы не принимали этот заказ!" });
            }

            _orderService.OrderCompleted(order);
            Logger.Log.Info($"{User.Identity.Name} перевел заказ №{order.OrderId} в статус \"Выполнен\"");
            return RedirectToAction("Details", new { id });
        }

        // POST: Admin/Order/Cancel
        [HttpPost]
        public ActionResult Cancel(int id)
        {
            var order = _orderService.GetOrder(id);
            if (order == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            if (order.ManagerId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Details", new { id, message = "Вы не принимали этот заказ!" });
            }

            _orderService.CancelOrder(order);
            Logger.Log.Info($"{User.Identity.Name} перевел заказ №{order.OrderId} в статус \"Отменен\"");
            return RedirectToAction("Details", new { id });
        }
    }
}