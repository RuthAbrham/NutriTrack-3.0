using Microsoft.AspNetCore.Identity;
using NutriTrackMVCApp.Models;
using System;

namespace NutriTrackMVCApp.Data
{
    public static class DatabaseInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            try
            {
                // Seed initial food data if it doesn't exist
                if (!context.Foods.Any())
                {
                    context.Foods.AddRange(
                        new Food
                        {
                            Name = "Gilde Lammekjøtt",
                            FoodGroup = "Meat",
                            Price = 81.9M,
                            Weight = 400,
                            ImageURL = "/images/foods/kjøttdeig-gilde.jpg"
                        },
                        new Food
                        {
                            Name = "Sørlands Chips",
                            FoodGroup = "Snacks",
                            Price = 33.9M,
                            Weight = 150,
                            ImageURL = "/images/foods/sørlandschips.jpg"
                        },
                        new Food
                        {
                            Name = "Pågen Cinnamon",
                            FoodGroup = "Bakery",
                            Price = 31.9M,
                            Weight = 300,
                            ImageURL = "/images/foods/gifflar_kanel.png"
                        });
                    context.SaveChanges();
                }

                // Seed roles if they don't exist
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                if (!await roleManager.RoleExistsAsync("User"))
                {
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }

                // Seed an admin user if it doesn't exist
                var adminEmail = "admin@example.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new IdentityUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(adminUser, "AdminPassword123!"); // Strong password
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
            }
        }
    }
}

