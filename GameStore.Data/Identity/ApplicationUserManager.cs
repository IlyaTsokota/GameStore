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
            Create();
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
       private void  Create()
        {

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
        }
    }
}
