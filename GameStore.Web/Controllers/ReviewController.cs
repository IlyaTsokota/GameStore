using AutoMapper;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.ViewModels;
using GameStore.Web.ViewModels.ReviewViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
        [Authorize]
        public class ReviewController : Controller
        {
            private readonly IReviewService _reviewService;

            private readonly IProductService _productService;

            public ReviewController(IReviewService reviewService, IProductService productService)
            {
                _reviewService = reviewService;
                _productService = productService;
            }

            // GET: /Review/GetReviews
            [AllowAnonymous]
            public ActionResult GetReviews(int productId, int page = 1)
            {
                var reviews = _reviewService.GetReviews(productId);
                var model = PagerViewModelHelper<ReviewViewModel>.ToPagerViewModel(reviews, page);
                return PartialView("_Reviews", model);
            }

            // POST: /Review/Create
            [HttpPost]
            public ActionResult Create(ReviewViewModel model)
            {
                var product = _productService.GeProduct(model.ProductId);
                if (product == null)
                {
                    return RedirectToAction("NotFound", "Error");
                }

                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Product", new { id = model.ProductId });
                }

                var review = Mapper.Map<ReviewViewModel, Review>(model);
                review.UserId = User.Identity.GetUserId();
                review.ReviewDate = DateTime.Now;
                _reviewService.CreateReview(review);
                Logger.Log.Info($"{User.Identity.Name} оставил комментарий к товару {model.ProductId}. Содержание комментария: \"{model.Text}\"");
                return RedirectToAction("Derails", "Products", new { id = model.ProductId });
            }

            // POST: /Review/Delete
            [HttpPost]
            public ActionResult Delete(int reviewId)
            {
                var review = _reviewService.GetReview(reviewId);
                if (review == null)
                {
                    return RedirectToAction("NotFound", "Error");
                }

                if (User.Identity.GetUserId() != review.UserId || !User.IsInRole("Manager"))
                {
                    return RedirectToAction("Forbidden", "Error");
                }

                _reviewService.DeleteReview(review);
                Logger.Log.Info($"{User.Identity.Name} удалил комментарий №{reviewId}");
                return RedirectToAction("Index", "Product", new { id = review.ProductId });
            }
        }
}