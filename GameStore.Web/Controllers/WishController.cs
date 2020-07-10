﻿using AutoMapper;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.ViewModels.WishViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
    [Authorize]
    public class WishController : Controller
    {
        private readonly IProductService _productService;

        private readonly IWishService _wishService;


        public WishController(IProductService productService, IWishService wishService)
        {
            _productService = productService;
            _wishService = wishService;
        }
        // GET: Wish
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var wishs = _wishService.GetWishes(userId);
            var model = wishs.Select(Mapper.Map<Wish, WishViewModel>).ToList();
            return View(model);
        }

        public ActionResult AddToWish(int productId)
        {
            var product = _productService.GeProduct(productId);
            var userId = User.Identity.GetUserId();
            int result;
            if (product != null)
            {
                _wishService.AddWishList(product, userId, out result);
            }
            else
            {
                return RedirectToAction("NotFound", "Error");
            }
            switch (result)
            {
                case 0:
                    return PartialView("_WishNotification");
                case 1:
                    return PartialView("_WishNotificationFailure");
                default:
                    return PartialView("_WishNotification");
            }
        }

        public ActionResult Delete(int wishId)
        {
            var wish = _wishService.GetWish(wishId);
            _wishService.Delete(wish);
            return RedirectToAction("Index");
        }

        public ActionResult Clear()
        {
            var userId = User.Identity.GetUserId();
            _wishService.Clear(userId);
            return RedirectToAction("Index");
        }
    }
}