using System.Security.Claims; // For accessing user ID
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; // Import for role-based authorization
using Microsoft.AspNetCore.Mvc;
using NutriTrackMVCApp.Data; // namespace
using NutriTrackMVCApp.Models; // Namespace for models
using NutriTrackMVCApp.Models; // Inkluderer modellen 'Food'
using NutriTrackMVCApp.Repositories; // Namespace for repository
using NutriTrackMVCApp.Services; // Namespace for services

namespace NutriTrackMVCApp.Controllers // Namespace for the controller
{
    public class FoodController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly NutriTrackMVCApp.Services.IAuthorizationService _authorizationService;
        private readonly ILogger<FoodController> _logger;

        // Constructor for IFoodRepository and IAuthorizationService
        public FoodController(
            IFoodRepository foodRepository,
            NutriTrackMVCApp.Services.IAuthorizationService authorizationService,
            ILogger<FoodController> logger
        )
        {
            _foodRepository = foodRepository;
            _authorizationService = authorizationService;
            _logger = logger;
        }

        // GET: /Food
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("GET /Food - Display all food items");
            var foods = await _foodRepository.GetAllFoodsAsync();
            return View(foods);
        }

        // GET: /Food/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation(
                $"GET /Food/Details/{id} - Display details of food item with ID: {id}"
            );

            var food = await _foodRepository.GetFoodByIdAsync(id);
            if (food == null)
            {
                _logger.LogWarning($"GET /Food/Details/{id} - Food item with ID: {id} not found");
                return NotFound();
            }
            return View(food);
        }

        // GET: /Food/Create
        // Display form to create a new food item
        public async Task<IActionResult> Create()
        {
            _logger.LogInformation("GET /Food/Create - Display form to create a new food item");
            // Authorization logic
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized(); // Redirect unauthorized users
            }
            return View();
        }

        // POST: /Food/Create
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
                _logger.LogInformation("POST /Food/Create - Create a new food item");
                await _foodRepository.AddFoodAsync(food);
                return RedirectToAction(nameof(Index));
            }
            _logger.LogWarning("POST /Food/Create - Model state is not valid");
            return View(food);
        }

        // GET: /Food/Edit/{id}
        // Display form to edit an existing food item
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation(
                $"GET /Food/Edit/{id} - Display form to edit food item with ID: {id}"
            );
            // Authorization logic
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized(); // Redirect unauthorized users
            }

            var food = await _foodRepository.GetFoodByIdAsync(id);
            if (food == null)
            {
                _logger.LogWarning($"GET /Food/Edit/{id} - Food item with ID: {id} not found");
                return NotFound();
            }
            return View(food);
        }

        // POST: /Food/Edit/{id}
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
                _logger.LogWarning(
                    $"POST /Food/Edit/{id} - Bad request, ID mismatch. Provided ID: {id}, Food ID: {food.Id}"
                );
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation($"POST /Food/Edit/{id} - Edit food item with ID: {id}");
                    await _foodRepository.UpdateFoodAsync(food);
                }
                catch
                {
                    if (await _foodRepository.GetFoodByIdAsync(id) == null)
                    {
                        _logger.LogWarning(
                            $"POST /Food/Edit/{id} - Food item with ID: {id} not found during update"
                        );
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            _logger.LogWarning($"POST /Food/Edit/{id} - Model state is not valid");
            return View(food);
        }

        // GET: /Food/Delete/{id}
        // Display confirmation page to delete a food item
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation(
                $"GET /Food/Delete/{id} - Display confirmation to delete food item with ID: {id}"
            );

            // Authorization logic
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized(); // Redirect unauthorized users
            }

            var food = await _foodRepository.GetFoodByIdAsync(id);
            if (food == null)
            {
                _logger.LogWarning($"GET /Food/Delete/{id} - Food item with ID: {id} not found");
                return NotFound();
            }
            return View(food);
        }

        // POST: /Food/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation(
                $"POST /Food/Delete/{id} - Confirm delete food item with ID: {id}"
            );
            // Authorization logic
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized(); // Redirect unauthorized users
            }

            await _foodRepository.DeleteFoodAsync(id);
            _logger.LogInformation($"POST /Food/Delete/{id} - Food Deleted Successfuly");

            return RedirectToAction(nameof(Index));
        }
    }
}
