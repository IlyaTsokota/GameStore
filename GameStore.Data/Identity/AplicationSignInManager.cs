
using GameStore.Model;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace GameStore.Data.Identity
{
    public class ApplicationSignInManager : SignInManager<User, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }
    }
}
