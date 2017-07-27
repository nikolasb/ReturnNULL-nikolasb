using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EachVoice.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string County { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("UCContext", throwIfV1Schema: false)
        {
            Database.SetInitializer <ApplicationDbContext> (null);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //***********make sure comment this field out since we do not need this instead of using default ApplicationUsers, which will return this same type of ApplciationUser
        //public System.Data.Entity.DbSet<EachVoice.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}