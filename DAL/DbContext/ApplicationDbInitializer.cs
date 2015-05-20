using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

            const string name = "admin";
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
                new Theme { Owner = user, Title = "Theme 1", CreationDateTime = DateTime.Now },
                new Theme { Owner = user, Title = "Theme 2", CreationDateTime = DateTime.Now },
                new Theme { Owner = user, Title = "Theme 3", CreationDateTime = DateTime.Now }
            };
            context.Themes.AddRange(themes);

            // Seed some Messages
            var theme = themes.First();
            var messages = new List<Message>
            {
                new Message { Text = "Message 1", Theme = theme, User = user, CreationDateTime = DateTime.Now },
                new Message { Text = "Message 2", Theme = theme, User = user, CreationDateTime = DateTime.Now }
            };
            context.Messages.AddRange(messages);

            base.Seed(context);
        }
    }
}