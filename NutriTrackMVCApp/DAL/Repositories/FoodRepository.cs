using NutriTrackMVCApp.Data;
using NutriTrackMVCApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutriTrackMVCApp.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly ApplicationDbContext _context;

        public FoodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Food>> GetAllFoodsAsync()
        {
            return await _context.Foods.ToListAsync();
        }

        public async Task<Food> GetFoodByIdAsync(int id)
        {
            return await _context.Foods.FindAsync(id);
        }

        public async Task AddFoodAsync(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFoodAsync(Food food)
        {
            _context.Foods.Update(food);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFoodAsync(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food != null)
            {
                _context.Foods.Remove(food);
                await _context.SaveChangesAsync();
            }
        }

        // Filter by food group
        public async Task<List<Food>> GetFoodsByFoodGroupAsync(string foodGroup)
        {
            return await _context.Foods
                .Where(f => f.FoodGroup == foodGroup)
                .ToListAsync();
        }

        // Search by name
        public async Task<List<Food>> SearchFoodsByNameAsync(string searchTerm)
        {
            return await _context.Foods
                .Where(f => f.Name.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
