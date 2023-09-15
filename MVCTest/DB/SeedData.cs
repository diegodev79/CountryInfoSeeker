namespace MVCTest.DB
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MVCTest.Models; // Replace with your application's namespace

    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create roles if they don't exist
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
            }

            // Create a user if it doesn't exist
            var user = userManager.FindByEmailAsync("diego.barrantes@gmail.com").Result;
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "diego.barrantes@gmail.com",
                    Email = "diego.barrantes@gmail.com",
                    FirstName = "Diego",
                    LastName = "Barrantes"
                };

                userManager.CreateAsync(user, "test1234").Wait();

                // Add the user to a role
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }

}
