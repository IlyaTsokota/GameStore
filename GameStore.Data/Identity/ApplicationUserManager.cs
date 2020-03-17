using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace GameStore.Data.Identity
{
    public class ApplicationUserManager : UserManager<User>
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        public ApplicationUserManager(IUserStore<User> store, IDataProtectionProvider dataProtectionProvider, IEmailService emailService, ISmsService smsService)
            : base(store)
        {
            _dataProtectionProvider = dataProtectionProvider;
            EmailService = emailService;
            SmsService = smsService;
            Create(new OwinContext());
        }

        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }


        public List<User> GetUsers(bool onlyBlocked = false)
        {
            var users = onlyBlocked
                ? Users.Where(user => user.LockoutEndDateUtc > DateTime.Now)
                : Users;
            return users.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
        }
        public ApplicationUserManager Create(IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<User>(context.Get<ApplicationContext.ApplicationContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            RegisterTwoFactorProvider(
                "Смс",
                new PhoneNumberTokenProvider<User, string>
                {
                    MessageFormat = "Ваш код -  {0}"
                });

            RegisterTwoFactorProvider(
                "Электронная почта",
                new EmailTokenProvider<User, string>
                {
                    Subject = "Ваш код",
                    BodyFormat = "Ваш код - {0}"
                });

            if (_dataProtectionProvider != null)
            {
                var dataProtector = _dataProtectionProvider.Create("ASP.NET Identity");
                UserTokenProvider = new DataProtectorTokenProvider<User, string>(dataProtector);
            }
            return manager;
        }
    }
}
