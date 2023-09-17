namespace MVCTest.DB
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MVCTest.Models; // Replace with your application's namespace

    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Create roles if they don't exist
                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
                }

                if (await userManager.FindByEmailAsync("diego.barrantes@gmail.com") == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = "diego.barrantes@gmail.com",
                        Email = "diego.barrantes@gmail.com",
                        FirstName = "Diego",
                        LastName = "Barrantes"
                    };
                    try
                    {
                        var result = await userManager.CreateAsync(user, "tesT1.2.34");
                        if (result.Succeeded)
                        {
                            // Assign the 'Admin' role to the user
                            await userManager.AddToRoleAsync(user, "Admin");
                        }
                        else
                        {
                            // Handle errors in result.Errors
                            foreach (var error in result.Errors)
                            {
                                // Log or display error messages
                                Console.WriteLine(error.Description);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }
    }

}
