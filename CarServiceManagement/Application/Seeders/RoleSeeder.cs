using Microsoft.AspNetCore.Identity;

namespace Application.Seeders;

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole<long>> roleManager)
    {
        string[] roles = ["Admin", "Owner", "User"];

        foreach (var roleName in roles)
        {
            if (await roleManager.RoleExistsAsync(roleName))
                continue;

            var result = await roleManager.CreateAsync(new IdentityRole<long>(roleName));

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(error => error.Description));

                throw new InvalidOperationException($"Cannot create role '{roleName}': {errors}");
            }
        }
    }
}