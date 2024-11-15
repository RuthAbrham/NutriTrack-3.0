using Microsoft.AspNetCore.Mvc;
using NutriTrackMVCApp.Data;  // namespace 
using NutriTrackMVCApp.Models;  // Inkluderer modellen 'Food'

namespace NutriTrackMVCApp.Controllers  // namespace for kontrolleren
{
    public class FoodController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FoodController> _logger;

        // Constructor for ApplicationDbContext to access the database and ILogger for logging
        public FoodController(ApplicationDbContext context, ILogger<FoodController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: /Food
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("GET /Food - Display all food items");
            var foods = await _context.Foods.ToListAsync();
            return View(foods);
        }

        // GET: /Food/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation($"GET /Food/Details/{id} - Display details of food item with ID: {id}");
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                _logger.LogWarning($"GET /Food/Details/{id} - Food item with ID: {id} not found");
                return NotFound();
            }
            return View(food);
        }

        // GET: /Food/Create
        public IActionResult Create()
        {
            _logger.LogInformation("GET /Food/Create - Display form to create a new food item");
            return View();
        }

        // POST: /Food/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Food food)
        {
            _logger.LogInformation("POST /Food/Create - Create a new food item");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Foods.Add(food);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("POST /Food/Create - Food item created successfully");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "POST /Food/Create - Error creating food item");
                }
            }
            _logger.LogWarning("POST /Food/Create - Model state is not valid");
            return View(food);
        }

        // GET: /Food/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation($"GET /Food/Edit/{id} - Display form to edit food item with ID: {id}");
            var food = await _context.Foods.FindAsync(id);
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
            _logger.LogInformation($"POST /Food/Edit/{id} - Edit food item with ID: {id}");
            if (id != food.Id)
            {
                _logger.LogWarning($"POST /Food/Edit/{id} - Bad request, ID mismatch. Provided ID: {id}, Food ID: {food.Id}");
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"POST /Food/Edit/{id} - Food item updated successfully");
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, $"POST /Food/Edit/{id} - Concurrency error updating food item");
                    if (!_context.Foods.Any(e => e.Id == id))
                    {
                        _logger.LogWarning($"POST /Food/Edit/{id} - Food item with ID: {id} not found during update");
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
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"GET /Food/Delete/{id} - Display confirmation to delete food item with ID: {id}");
            var food = await _context.Foods.FindAsync(id);
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
            _logger.LogInformation($"POST /Food/Delete/{id} - Confirm delete food item with ID: {id}");
            var food = await _context.Foods.FindAsync(id);
            if (food != null)
            {
                _context.Foods.Remove(food);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"POST /Food/Delete/{id} - Food item deleted successfully");
            }
            else
            {
                _logger.LogWarning($"POST /Food/Delete/{id} - Food item with ID: {id} not found during delete confirmation");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
