using BlogApp.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogApp.Identity
{
    public class IdentityInitializer : DropCreateDatabaseIfModelChanges<IdentityDbContext>
    {
        protected override void Seed(IdentityDbContext context)
        {
            //Roller
            if(!context.Roles.Any(i => i.Name == "admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new  RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "admin", Description = "yönetici rolü" };
                manager.Create(role);
            }

            if (!context.Roles.Any(i => i.Name == "author"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "author", Description = "yazar rolü" };
                manager.Create(role);
            }

            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "user", Description = "kullanıcı rolü" };
                manager.Create(role);
            }



            //Kullanıcılar
            if (!context.Users.Any(i => i.Name == "User"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Name = "Admin", Surname = "Admin", Email = "user1@gmail.com", UserName = "Admin", Url= "Admin.jpg", Description="Açıklama" };
                manager.Create(user, "user123");
                manager.AddToRole(user.Id, "admin");
            }

            base.Seed(context);
        }


    }
}