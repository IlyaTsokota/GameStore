using AutoMapper;
using GameStore.Data.Identity;
using GameStore.Model;
using GameStore.Service;
using GameStore.Web.Areas.Admin.ViewModels.OrderViewModels;
using GameStore.Web.Extensions;
using GameStore.Web.ViewModels;
using GameStore.Web.ViewModels.ProfileViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IOrderService _orderService;

        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        public ProfileController(IOrderService orderService, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _orderService = orderService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public enum ManageMessageId
        {
            ConfirmPhoneSuccess,
            ChangePasswordSuccess,
            EditContactInfoSuccess,
            SetPasswordSuccess,
            Error
        }
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
              message == ManageMessageId.ChangePasswordSuccess ? "Пароль успешно изменен!"
              : message == ManageMessageId.EditContactInfoSuccess ? "Информация изменена"
              : message == ManageMessageId.SetPasswordSuccess ? "Пароль изменен."
              : message == ManageMessageId.Error ? "Произошла ошибка!"
              : message == ManageMessageId.ConfirmPhoneSuccess ? "Номер телефона подтвержден."
              : string.Empty;
            var userId = User.Identity.GetUserId();
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            var model = Mapper.Map<User, ProfileViewModel>(user);
            return View(model);
        }

        [HttpGet]
        public ActionResult GetOrders(int? statusId = null)
        {
            var customerId = User.Identity.GetUserId();
            var orders = _orderService.GetOrders(statusId: statusId, customerId: customerId);
            var model = orders.Select(Mapper.Map<Order, OrderViewModel>).ToList();
            return View(model);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Edit(ContactInfoViewModel model)
        {
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId()).ConfigureAwait(false);
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            if (user.PhoneNumber != model.Phone)
            {
                user.PhoneNumber = model.Phone;
                user.PhoneNumberConfirmed = false;
            }

            var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
            if (result.Succeeded)
            {
                Logger.Log.Info($"{User.Identity.Name} изменил информацию о себе");
                return RedirectToAction("Index", new { Message = ManageMessageId.EditContactInfoSuccess });
            }

            ModelState.AddModelErrors(result.Errors.Select(error => new ValidationResult(error)));
            return View(model);
        }

        // POST: /Settings/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.Password, model.NewPassword).ConfigureAwait(false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId()).ConfigureAwait(false);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                Logger.Log.Info($"{User.Identity.Name} изменил пароль");
                await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false).ConfigureAwait(false);

                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelErrors(result.Errors.Select(error => new ValidationResult(error)));
            return View(model);
        }
        public async Task<ActionResult> VerifyEmail()
        {
            var userId = User.Identity.GetUserId();
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id).ConfigureAwait(false);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, protocol: Request.Url.Scheme);
            await _userManager.SendEmailAsync(user.Id, "Подтвердите электронную почту", "Чтобы подтвердить электронную почту <a href=\"" + callbackUrl + "\">кликните здесь</a>").ConfigureAwait(false);
            return View("VerifyEmailConfirmation");
        }

        // GET: /Settings/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber()
        {
            var userId = User.Identity.GetUserId();
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            var code = await _userManager.GenerateTwoFactorTokenAsync(userId, user.PhoneNumber).ConfigureAwait(false);
            if (_userManager.SmsService == null)
            {
                return user.PhoneNumber == null
                           ? View("Error")
                           : View(new VerifyPhoneNumberViewModel { PhoneNumber = user.PhoneNumber });
            }

            var message = new IdentityMessage
            {
                Destination = user.PhoneNumber,
                Body = "Ваш код: " + code
            };
            await _userManager.SmsService.SendAsync(message).ConfigureAwait(false);
            Logger.Log.Info($"Пользователю {user.UserName} было отправлено сообщение для подтверждения номера телефона");

            // Send an SMS through the SMS provider to verify the phone number
            return user.PhoneNumber == null
                       ? View("Error")
                       : View(new VerifyPhoneNumberViewModel { PhoneNumber = user.PhoneNumber });
        }

        // POST: /Settings/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code).ConfigureAwait(false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId()).ConfigureAwait(false);
                if (user == null)
                {
                    return RedirectToAction("Index", new { Message = ManageMessageId.ConfirmPhoneSuccess });
                }

                await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false).ConfigureAwait(false);
                Logger.Log.Info($"Пользователь {user.UserName} подтвердил номер телефона");

                return RedirectToAction("Index", new { Message = ManageMessageId.ConfirmPhoneSuccess });
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError(string.Empty, @"Произошла ошибка.");
            return View(model);
        }

        // POST: /Settings/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await _userManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true).ConfigureAwait(false);
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId()).ConfigureAwait(false);
            if (user == null)
            {
                return RedirectToAction("Index", "Profile");
            }

            await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false).ConfigureAwait(false);
            Logger.Log.Info($"{user.UserName} включил двухфакторную аутентификацию");

            return RedirectToAction("Index", "Profile");
        }

        // POST: /Settings/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await _userManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false).ConfigureAwait(false);
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId()).ConfigureAwait(false);
            if (user == null)
            {
                return RedirectToAction("Index", "Profile");
            }

            await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false).ConfigureAwait(false);
            Logger.Log.Info($"{user.UserName} выключил двухфакторную аутентификацию");
            return RedirectToAction("Index", "Profile");
        }
    }


}