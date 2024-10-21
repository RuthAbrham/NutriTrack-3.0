using System.ComponentModel.DataAnnotations;

namespace NutriTrackMVCApp.Models  // namespace 
{
    public class Food
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string FoodGroup { get; set; }

        public double Energy { get; set; }
        public double Fat { get; set; }
        public double Carbohydrates { get; set; }
        public double Protein { get; set; }
    }
}
