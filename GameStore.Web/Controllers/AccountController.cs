
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Coco.Web.Mvc;
using GameStore.Data.Identity;
using GameStore.Model;
using GameStore.Web.Extensions;
using GameStore.Web.ViewModels.AccountViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Extensions.Logging;
using Microsoft.Owin.Security;
using static GameStore.Web.Extensions.ValidationResultExtension;

namespace GameStore.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [OnlyAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, MiddleName = model.MiddleName, LastName = model.LastName, PhoneNumber = model.Phone };
                var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    var currentUser = await _userManager.FindByEmailAsync(user.Email).ConfigureAwait(false);
                    await _userManager.AddToRoleAsync(currentUser.Id, "User").ConfigureAwait(false);
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: true).ConfigureAwait(false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id).ConfigureAwait(false);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, protocol: Request.Url.Scheme);
                    //await _userManager.SendEmailAsync(
                    //        user.Id,
                    //        "Подтвердите электронную почту",
                    //        "Чтобы подтвердить регистрацию <a href=\"" + callbackUrl + "\">кликните здесь</a>")
                    //    .ConfigureAwait(false);


                    return RedirectToAction("Index", "Home");
                };

                ModelState.AddModelErrors(result.Errors.Select(x => new ValidationResult(x)));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(userId, code).ConfigureAwait(false);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        public ActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
         
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                //case SignInStatus.RequiresVerification:
                //    return RedirectToAction("SendCode", new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Logout()
        { 
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ConfirmEmail()
        {
            return View();
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
    internal class ChallengeResult : HttpUnauthorizedResult
    {
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        public ChallengeResult(string provider, string redirectUri)
            : this(provider, redirectUri, null)
        {
        }

        public ChallengeResult(string provider, string redirectUri, string userId)
        {
            LoginProvider = provider;
            RedirectUri = redirectUri;
            UserId = userId;
        }

        public string LoginProvider { get; set; }

        public string RedirectUri { get; set; }

        public string UserId { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
            if (UserId != null)
            {
                properties.Dictionary[XsrfKey] = UserId;
            }

            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        }
    }


}