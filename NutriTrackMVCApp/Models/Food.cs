using System.ComponentModel.DataAnnotations;

namespace NutriTrackMVCApp.Models  // namespace 
{
    public class Food
    {
        public int Id { get; set; }

        // Basic Information
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public required string Name { get; set; }

        [Required]
        public required string FoodGroup { get; set; }
        
        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }  // Price of the item

        [Required(ErrorMessage = "Weight is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Weight must be a positive number.")]
        public double Weight { get; set; } // Weight in grams

        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string? ImageURL { get; set; }  // URL for the product image

        // Nutritional Values per 100g
        [Range(0, double.MaxValue, ErrorMessage = "Energy must be a positive number.")]
        public double Energy { get; set; }  // in kcal

        [Range(0, double.MaxValue, ErrorMessage = "Fat must be a positive number.")]
        public double Fat { get; set; }  // in grams

        [Range(0, double.MaxValue, ErrorMessage = "Saturated Fat must be a positive number.")]
        public double SaturatedFat { get; set; } // in grams

        [Range(0, double.MaxValue, ErrorMessage = "Carbohydrates must be a positive number.")]
        public double Carbohydrates { get; set; }  // in grams

        [Range(0, double.MaxValue, ErrorMessage = "Sugar must be a positive number.")]
        public double Sugar { get; set; }  // in grams

        [Range(0, double.MaxValue, ErrorMessage = "Protein must be a positive number.")]
        public double Protein { get; set; }  // in grams

        [Range(0, double.MaxValue, ErrorMessage = "Salt must be a positive number.")]
        public double Salt { get; set; }  // in grams

        
    }
}

