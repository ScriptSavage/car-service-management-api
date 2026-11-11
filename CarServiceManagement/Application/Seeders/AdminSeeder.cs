using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.Seeders;

public static class AdminSeeder
{
    public static async Task AddAdmin(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        var admin = configuration.GetSection("AdminCredentials");

        var appAdmin = new ApplicationUser
        {
            UserName = admin["Login"],
            Email = admin["Email"],
            FirstName = "Admin",
            LastName = "Admin",
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var doesEmailAlreadyExist = await userManager.FindByEmailAsync(admin["Email"]);
        var userNameAlreadyExist = await userManager.FindByNameAsync(appAdmin.UserName);
        if (doesEmailAlreadyExist is null && userNameAlreadyExist is null)
        {
            var resultCreateUser = await userManager.CreateAsync(appAdmin, admin["Password"]);

            if (!resultCreateUser.Succeeded)
            {
                var errors = string.Join("; ", resultCreateUser.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"{errors}");
            }

            var resultAddToRole = await userManager.AddToRoleAsync(appAdmin, "Admin");
            if (!resultAddToRole.Succeeded)
            {
                var errors = string.Join("; ", resultAddToRole.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"{errors}");
            }   
        }
        
        
        

    }
}