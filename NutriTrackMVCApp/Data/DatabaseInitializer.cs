using NutriTrackMVCApp.Data;
using NutriTrackMVCApp.Models;

namespace NutriTrackMVCApp.Data
{
    public static class DatabaseInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            try{
                if (!context.Foods.Any())
                {
                    context.Foods.AddRange(
                        new Food { Name = "Gilde Lammekjøtt", FoodGroup = "Meat", Price = 81.9M, Weight = 400, ImageURL = "/images/foods/kjøttdeig-gilde.jpg" },
                        new Food { Name = "Sørlands Chips", FoodGroup = "Snacks", Price = 33.9M, Weight = 150, ImageURL = "/images/foods/sørlandschips.jpg" },
                        new Food { Name = "Pågen Cinnamon", FoodGroup = "Bakery", Price = 31.9M, Weight = 300, ImageURL = "/images/foods/gifflar_kanel.png" });
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
