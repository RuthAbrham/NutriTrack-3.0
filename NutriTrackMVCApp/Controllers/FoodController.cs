using NutriTrackMVCApp.Models;  // Namespace for models
using NutriTrackMVCApp.Repositories;  // Namespace for repository
using NutriTrackMVCApp.Services;  // Namespace for services
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;  // Import for role-based authorization
using System.Security.Claims; // For accessing user ID
using System.Threading.Tasks;

namespace NutriTrackMVCApp.Controllers  // Namespace for the controller
{
    public class FoodController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly NutriTrackMVCApp.Services.IAuthorizationService _authorizationService;

        // Constructor for IFoodRepository and IAuthorizationService
        public FoodController(IFoodRepository foodRepository, NutriTrackMVCApp.Services.IAuthorizationService authorizationService)
        {
            _foodRepository = foodRepository;
            _authorizationService = authorizationService;
        }

        // GET: /Food
        // Display all food items (Index page)
        public async Task<IActionResult> Index()
        {
            var foods = await _foodRepository.GetAllFoodsAsync();
            return View(foods);
        }

        // GET: /Food/Details/{id}
        // Display details of a specific food item
        public async Task<IActionResult> Details(int id)
        {
            var food = await _foodRepository.GetFoodByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // GET: /Food/Create
        // Display form to create a new food item
        public async Task<IActionResult> Create()
        {
            // Authorization logic
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized(); // Redirect unauthorized users
            }

            return View();
        }

        // POST: /Food/Create
        // Handle form submission to create a new food item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Food food)
        {
            // Authorization logic
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized(); // Redirect unauthorized users
            }

            if (ModelState.IsValid)
            {
                await _foodRepository.AddFoodAsync(food);
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: /Food/Edit/{id}
        // Display form to edit an existing food item
        public async Task<IActionResult> Edit(int id)
        {
            // Authorization logic
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized(); // Redirect unauthorized users
            }

            var food = await _foodRepository.GetFoodByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: /Food/Edit/{id}
        // Handle form submission to update an existing food item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Food food)
        {
            // Authorization logic
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized(); // Redirect unauthorized users
            }

            if (id != food.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _foodRepository.UpdateFoodAsync(food);
                }
                catch
                {
                    if (await _foodRepository.GetFoodByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: /Food/Delete/{id}
        // Display confirmation page to delete a food item
        public async Task<IActionResult> Delete(int id)
        {
            // Authorization logic
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized(); // Redirect unauthorized users
            }

            var food = await _foodRepository.GetFoodByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: /Food/Delete/{id}
        // Handle confirmation to delete a food item
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Authorization logic
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized(); // Redirect unauthorized users
            }

            await _foodRepository.DeleteFoodAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}


