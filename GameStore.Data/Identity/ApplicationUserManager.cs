using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Data.Infrastructure;
using GameStore.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace GameStore.Data.Identity
{
    public class ApplicationUserManager : UserManager<User>
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        public ApplicationUserManager(IUserStore<User> store, IDataProtectionProvider dataProtectionProvider)
            : base(store)
        {
            _dataProtectionProvider = dataProtectionProvider;
            CreateApplicationUserManager();
        }
        public List<User> GetUsers(bool onlyBlocked = false)
        {
            var users = onlyBlocked
                ? Users.Where(user => user.LockoutEndDateUtc > DateTime.Now)
                : Users;
            return users.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
        }
        private void CreateApplicationUserManager()
        {
            if (_dataProtectionProvider != null)
            {
                var dataProtector = _dataProtectionProvider.Create("ASP.NET Identity");
                UserTokenProvider = new DataProtectorTokenProvider<User, string>(dataProtector);
            }
        }
    }
}
