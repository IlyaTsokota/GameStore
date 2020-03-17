
using GameStore.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GameStore.Data.Identity
{
    public class ApplicationUserStore : UserStore<User>
    {
        public ApplicationUserStore(ApplicationContext.ApplicationContext context)
            : base(context)
        {
        }
    }
}
