using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriTrackMVCApp.Models;
using NutriTrackMVCApp.Repositories;
using System.Security.Claims;

namespace NutriTrackMVCApp.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly NutriTrackMVCApp.Services.IAuthorizationService _authorizationService;
        private readonly ILogger<FoodController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FoodController(
            IFoodRepository foodRepository,
            NutriTrackMVCApp.Services.IAuthorizationService authorizationService,
            ILogger<FoodController> logger,
            IWebHostEnvironment webHostEnvironment
        )
        {
            _foodRepository = foodRepository;
            _authorizationService = authorizationService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: /Food
        [AllowAnonymous]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            _logger.LogInformation("GET /Food/Create - Display form to create a new food item");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized();
            }
            return View();
        }

        // POST: /Food/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Food food, IFormFile imageFile)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/foods");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    food.ImageURL = "/images/foods/" + uniqueFileName;
                }

                await _foodRepository.AddFoodAsync(food);
                return RedirectToAction(nameof(Index));
            }

            return View(food);
        }

        // GET: /Food/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation(
                $"GET /Food/Edit/{id} - Display form to edit food item with ID: {id}"
            );
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized();
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
        public async Task<IActionResult> Edit(int id, Food food, IFormFile imageFile)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized();
            }

            if (id != food.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/foods");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    food.ImageURL = "/images/foods/" + uniqueFileName;
                }

                await _foodRepository.UpdateFoodAsync(food);
                return RedirectToAction(nameof(Index));
            }

            return View(food);
        }

        // GET: /Food/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation(
                $"GET /Food/Delete/{id} - Display confirmation to delete food item with ID: {id}"
            );

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized();
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation(
                $"POST /Food/Delete/{id} - Confirm delete food item with ID: {id}"
            );
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _authorizationService.IsAdmin(userId))
            {
                return Unauthorized();
            }

            await _foodRepository.DeleteFoodAsync(id);
            _logger.LogInformation($"POST /Food/Delete/{id} - Food Deleted Successfully");

            return RedirectToAction(nameof(Index));
        }
    }
}
