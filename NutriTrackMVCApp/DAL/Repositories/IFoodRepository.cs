using NutriTrackMVCApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutriTrackMVCApp.Repositories
{
    public interface IFoodRepository
    {
        Task<List<Food>> GetAllFoodsAsync();
        Task<Food> GetFoodByIdAsync(int id);
        Task AddFoodAsync(Food food);
        Task UpdateFoodAsync(Food food);
        Task DeleteFoodAsync(int id);

        // Additional methods for enhanced functionality
        Task<List<Food>> GetFoodsByFoodGroupAsync(string foodGroup); // Filter by food group
        Task<List<Food>> SearchFoodsByNameAsync(string searchTerm);  // Search by name
    }
}

