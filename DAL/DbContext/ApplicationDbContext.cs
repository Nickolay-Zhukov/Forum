using System.Configuration;
using System.Data.Entity;
using Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["ForumDbConnection"].ConnectionString;
        public ApplicationDbContext() : base(ConnectionString) { }

        public DbSet<Theme> Themes { get; set; }

        public System.Data.Entity.DbSet<Core.Models.Message> Messages { get; set; }
    }
}