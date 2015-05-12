using System.Collections.Generic;
using System.Data.Entity;
using Core.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.DbContext
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            const string name = "Admin";
            const string password = "Adm123_";

            // Create Role Admin if it doesn't exist
            if (!roleManager.RoleExists(name)) roleManager.Create(new IdentityRole(name));

            // Create User=Admin with password=Adm123_
            var user = new ApplicationUser { UserName = name };
            var adminresult = userManager.Create(user, password);

            // Add User Admin to Role Admin
            if (adminresult.Succeeded) userManager.AddToRole(user.Id, name);

            // Seed some Themes
            var themes = new List<Theme>
            {
                new Theme { Title = "Theme 1", Owner = user},
                new Theme { Title = "Theme 2", Owner = user },
                new Theme { Title = "Theme 3", Owner = user }
            };
            context.Themes.AddRange(themes);

            base.Seed(context);
        }
    }
}