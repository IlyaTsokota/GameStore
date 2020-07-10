using AutoMapper;
using GameStore.Data.Identity;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.Areas.Admin.ViewModels.UserViewModels;
using GameStore.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ApplicationUserManager _userManager;
        private const int _pageSize = 10;

        public CustomerController(ApplicationUserManager userManager, IOrderService orderService)
        {
            _userManager = userManager;
            _orderService = orderService;
        }

        // GET: Admin/Customer/Index
        public ActionResult Index(int page = 1)
        {
            var customers = _userManager.Users.ToList();
            var mappingCustomers = customers.Select(Mapper.Map<User, UserViewModel>).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            var pager = new Pager(page, customers.Count, _pageSize);
            var model = new IndexUserViewModel
            {
                Users = mappingCustomers,
                Pager = pager
            };
            return View(model);
        }

        // GET: Admin/Customer/GetUsers
        public ActionResult GetUsers(bool onlyBlocked = false, int page = 1)
        {
            var customers = _userManager.GetUsers(onlyBlocked);
            var mappingCustomers = customers.Select(Mapper.Map<User, UserViewModel>).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
            var pager = new Pager(page, customers.Count, _pageSize);
            var model = new IndexUserViewModel
            {
                Users = mappingCustomers,
                Pager = pager
            };
            return PartialView("_Customers", model);
        }

        // GET: Admin/Customer/Details
        public ActionResult Details(string id, string msg = null)
        {
            ViewData["Message"] = msg;
            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            user.Orders = _orderService.GetOrders(customerId: user.Id);
            var model = Mapper.Map<User, DetailsUserViewModel>(user);
            model.IsBlocked = user.LockoutEndDateUtc > DateTime.Now;
            return View(model);
        }

        // POST: Admin/Customer/Block
        [HttpPost]
        public async Task<ActionResult> Block(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            if (_userManager.IsInRole(id, "Manager"))
            {
                return RedirectToAction("Details", new { id, msg = "Пользователь является менеджером" });
            }

            await _userManager.SetLockoutEnabledAsync(user.Id, true).ConfigureAwait(false);
            await _userManager.SetLockoutEndDateAsync(user.Id, DateTimeOffset.MaxValue).ConfigureAwait(false);
            await _userManager.UpdateSecurityStampAsync(user.Id).ConfigureAwait(false);
            Logger.Log.Info($"{User.Identity.Name} заблокировал пользователя {user.UserName}");
            return RedirectToAction("Details", new { id });
        }

        // POST: Admin/Customer/Unblock
        [HttpPost]
        public async Task<ActionResult> Unblock(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null)
            {
                return RedirectToAction("NotFound", "Error", new { Area = string.Empty });
            }

            await _userManager.SetLockoutEndDateAsync(user.Id, DateTimeOffset.Now).ConfigureAwait(false);
            Logger.Log.Info($"{User.Identity.Name}  разблокировал пользователя {user.UserName}");
            return RedirectToAction("Details", new { id });
        }
    }
}