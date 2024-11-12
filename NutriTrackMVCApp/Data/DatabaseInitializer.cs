using NutriTrackMVCApp.Data;
using NutriTrackMVCApp.Models;

namespace NutriTrackMVCApp.Data
{
    public static class DatabaseInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Foods.Any())
            {
                context.Foods.AddRange(
                    new Food { Name = "Gilde Lammekjøtt", FoodGroup = "Meat", Price = 81.9M, Weight = 400, ImageURL = "https://www.gilde.no/assets/productImages/_productImage/ujvjdur62u.jpg" },
                    new Food { Name = "Sørlands Chips", FoodGroup = "Snacks", Price = 33.9M, Weight = 150, ImageURL = "https://meny.no/varer/snacks-godteri/snacks/chips/sorlandschips-7071688004171" },
                    new Food { Name = "Pågen Cinnamon", FoodGroup = "Bakery", Price = 31.9M, Weight = 300, ImageURL = "https://www.google.com/imgres?q=P%C3%A5gen%20Cinnamon&imgurl=https%3A%2F%2Fpagen.com%2Fglobalassets%2Fprodukter%2Fpackshots%2Fgifflar_kanel_int_2020.png%3Fw%3D320%26h%3D320%26mode%3Dcrop%26resized%3Dtrue&imgrefurl=https%3A%2F%2Fpagen.com%2Four-range%2Fgifflar%2Fgifflar-cinnamon%2F&docid=M8I6zIpSpQwnUM&tbnid=W3GAipgKIUBjsM&vet=12ahUKEwj78YLJx8OJAxWoHxAIHaVWEe0QM3oECGMQAA..i&w=320&h=320&hcb=2&ved=2ahUKEwj78YLJx8OJAxWoHxAIHaVWEe0QM3oECGMQAA" }
                );
                context.SaveChanges();
            }
        }
    }
}
