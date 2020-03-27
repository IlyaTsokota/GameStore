
using System.Collections.Generic;

using System.Data.Entity;

using GameStore.Model;

using Microsoft.AspNet.Identity;

using Microsoft.AspNet.Identity.EntityFramework;

namespace GameStore.Data.ApplicationContext
{
    public class ApplicationContextInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var orderStatuses = new List<OrderStatus>
            {
                new OrderStatus {Name = "Зарегистрирован"},
                new OrderStatus {Name = "Оплата разрешена"},
                new OrderStatus {Name = "Оплачен"},
                new OrderStatus {Name = "Выполнен"},
                new OrderStatus {Name = "Отменен"},
            };
            orderStatuses.ForEach(status => context.OrderStatus.Add(status));

            var roles = new List<IdentityRole>
            {
                new IdentityRole("User"),
                new IdentityRole("Manager"),
                new IdentityRole("Admin")
            };
            roles.ForEach(role => context.Roles.Add(role));

            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);
            var admin = new User { Email = "gamestore@gmail.com", UserName = "gamestore@gmail.com", FirstName = "Илья", MiddleName = "Цокота", LastName = "Олегович", PhoneNumber = "+380990482560" };
            string password = "13Avtobusus";
            var result = manager.Create(admin, password);
            if (result.Succeeded)
            {
                manager.AddToRole(admin.Id, "Admin");
            }
            base.Seed(context);
        }
    }
}
