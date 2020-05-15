
using Microsoft.AspNet.Identity.EntityFramework;

namespace GameStore.Data.Identity
{
    public class ApplicationRoleStore : RoleStore<IdentityRole>
    {
        public ApplicationRoleStore(ApplicationContext.ApplicationContext context) : base(context)
        {

        }
    }
}
