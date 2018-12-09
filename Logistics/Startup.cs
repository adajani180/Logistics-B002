using Logistics.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(Logistics.Startup))]
namespace Logistics
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
            CreateAdmin();
            //AddToAdmin();
        }

        // In this method we will create default User roles and Admin user for login   
        private void CreateRolesandUsers()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                // Create Roles witihin Identity    
                if (!roleManager.RoleExists("Admin"))
                {
                    // Create Admin role   
                    var role = new IdentityRole { Name = "Admin" };
                    roleManager.Create(role);
                }

                // Creating Manager role    
                if (!roleManager.RoleExists("Manager"))
                {
                    var role = new IdentityRole { Name = "Manager" };
                    roleManager.Create(role);
                }

                // Creating Employee role    
                if (!roleManager.RoleExists("Employee"))
                {
                    var role = new IdentityRole { Name = "Employee" };
                    roleManager.Create(role);
                }
            }                
        }

        /// <summary>
        /// Create an Admin user if doesnt exist.
        /// </summary>
        private void CreateAdmin()
        {
            var user = new ApplicationUser
            {
                UserName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };

            using (var cxt = new ApplicationDbContext())
            {
                try
                {
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(cxt));
                    var admin = userManager.FindByName(user.UserName);
                    if (admin == null)
                    {
                        var result = userManager.Create(user, "satisapp");
                        if (result.Succeeded)
                        {
                            userManager.AddToRole(user.Id, "Admin");
                            cxt.SaveChanges();
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        private void AddToAdmin()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string userName = "Ahmed";
            string roleName = "Admin";

            try
            {
                var user = UserManager.FindByName(userName);
                UserManager.AddToRole(user.Id, roleName);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    
}