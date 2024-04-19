
using Key_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Key_Management_System.Configuration;

namespace Key_Management_System.Services.Initialize
{
    public static class ConfigureIdentity
    {
        public static async Task ConfigureIdentityAsync(this WebApplication app)
        {
            using var serviceScope = app.Services.CreateScope();
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();

            var config = app.Configuration.GetSection("AdminCredentials");
            // Creation of role
            var adminRole = await roleManager.FindByNameAsync(ApplicationRoleNames.Administrator);
            if (adminRole == null)
            {
                var roleResult = await roleManager.CreateAsync(new Role
                {
                    Name = ApplicationRoleNames.Administrator
                });
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to create role {ApplicationRoleNames.Administrator}");
                }

                adminRole = await roleManager.FindByNameAsync(ApplicationRoleNames.Administrator);
            }

            // Creation of user

            /*var adminUser = await userManager.FindByEmailAsync(config["Email"]);
            if (adminUser == null)
            {
                var userResult = await userManager.CreateAsync(new User
                {
                    UserName = config["Email"],
                    Email = config["Email"],
                    //FullName = "Administrator",
                }, config["Password"]);

                if (!userResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unable to create user with email={config["Email"]}");
                }
                adminUser = await userManager.FindByEmailAsync(config["Email"]);
            }

            if (!await userManager.IsInRoleAsync(adminUser, adminRole.Name))
            {
                await userManager.AddToRoleAsync(adminUser, adminRole.Name);
            }*/
        }
    }
}
