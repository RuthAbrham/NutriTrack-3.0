using System;
using System.Collections.Generic;
using System.Linq;
using NutriTrackMVCApp.Models;
using NutriTrackMVCApp.Data;

namespace NutriTrackMVCApp.Services
{
    public class FoodService
    {
        private readonly ApplicationDbContext _context;

        public FoodService(ApplicationDbContext context)
        {
            _context = context;
        }

        // **Validation Logic**
        public void ValidateFood(Food food)
        {
            if (string.IsNullOrWhiteSpace(food.Name))
                throw new ArgumentException("Food name cannot be empty.");

            if (food.Price < 0)
                throw new ArgumentException("Price must be a positive number.");

            if (food.Weight < 0)
                throw new ArgumentException("Weight must be a positive number.");

            if (food.Fat < food.SaturatedFat)
                throw new ArgumentException("Fat cannot be less than saturated fat.");

            if (food.Carbohydrates < food.Sugar)
                throw new ArgumentException("Carbohydrates cannot be less than sugar.");

            if (_context.Foods.Any(f => f.Name == food.Name && f.FoodGroup == food.FoodGroup))
                throw new ArgumentException("A food item with the same name and food group already exists.");
        }

        // **Business Rule: Healthy Food Label**
        public bool IsHealthyFood(Food food)
        {
            // Example rule for "Healthy Food"
            return food.Fat <= 3 && food.SaturatedFat <= 1.5 && food.Salt <= 1.2;
        }

        // **CRUD Operations (Business Logic Layer)**
        public List<Food> GetAllFoods()
        {
            return _context.Foods.ToList();
        }

        public Food GetFoodById(int id)
        {
            var food = _context.Foods.Find(id);
            if (food == null)
                throw new ArgumentException("Food not found.");
            return food;
        }

        public void AddFood(Food food)
        {
            ValidateFood(food);
            _context.Foods.Add(food);
            _context.SaveChanges();
        }

        public void UpdateFood(int id, Food updatedFood)
        {
            var existingFood = _context.Foods.Find(id);
            if (existingFood == null)
                throw new ArgumentException("Food not found.");

            ValidateFood(updatedFood);

            // Update fields
            existingFood.Name = updatedFood.Name;
            existingFood.Price = updatedFood.Price;
            existingFood.Weight = updatedFood.Weight;
            existingFood.ImageURL = updatedFood.ImageURL;
            existingFood.FoodGroup = updatedFood.FoodGroup;
            existingFood.Energy = updatedFood.Energy;
            existingFood.Fat = updatedFood.Fat;
            existingFood.SaturatedFat = updatedFood.SaturatedFat;
            existingFood.Carbohydrates = updatedFood.Carbohydrates;
            existingFood.Sugar = updatedFood.Sugar;
            existingFood.Protein = updatedFood.Protein;
            existingFood.Salt = updatedFood.Salt;

            _context.SaveChanges();
        }

        public void DeleteFood(int id)
        {
            var food = _context.Foods.Find(id);
            if (food == null)
                throw new ArgumentException("Food not found.");

            _context.Foods.Remove(food);
            _context.SaveChanges();
        }
    }
}
