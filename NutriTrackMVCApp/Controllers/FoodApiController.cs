using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriTrackMVCApp.Data;  // namespace 
using NutriTrackMVCApp.Models;  // Inkluderer modellen 'Food'

namespace NutriTrackMVCApp.Controllers  // namespace for kontrolleren
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FoodApiController> _logger;

        // Constructor for ApplicationDbContext and ILogger for logging
        public FoodApiController(ApplicationDbContext context, ILogger<FoodApiController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/food
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetAllFoods()
        {
            _logger.LogInformation("GET api/food - Retrieve all food items");
            return await _context.Foods.ToListAsync();
        }

        // GET: api/food/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            _logger.LogInformation($"GET api/food/{id} - Retrieve food item with ID: {id}");
            var food = await _context.Foods.FindAsync(id);

            if (food == null)
            {
                _logger.LogWarning($"GET api/food/{id} - Food item with ID: {id} not found");
                return NotFound();
            }

            return food;
        }

        // POST: api/food
        [HttpPost]
        public async Task<ActionResult<Food>> CreateFood(Food food)
        {
            _logger.LogInformation("POST api/food - Create a new food item");
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"POST api/food - Food item with ID: {food.Id} created successfully");

            return CreatedAtAction(nameof(GetFood), new { id = food.Id }, food);
        }

        // PUT: api/food/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFood(int id, Food food)
        {
            _logger.LogInformation($"PUT api/food/{id} - Update food item with ID: {id}");
            if (id != food.Id)
            {
                _logger.LogWarning($"PUT api/food/{id} - Bad request, ID mismatch. URL ID: {id}, Food ID: {food.Id}");
                return BadRequest("Id in URL does not match Id in body.");
            }

            _context.Entry(food).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"PUT api/food/{id} - Food item with ID: {id} updated successfully");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Foods.Any(e => e.Id == id))
                {
                    _logger.LogWarning($"PUT api/food/{id} - Food item with ID: {id} not found during update");
                    return NotFound("Food item not found.");
                }
                else
                {
                    _logger.LogError($"PUT api/food/{id} - Concurrency error updating food item with ID: {id}");
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/food/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            _logger.LogInformation($"DELETE api/food/{id} - Delete food item with ID: {id}");
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                _logger.LogWarning($"DELETE api/food/{id} - Food item with ID: {id} not found");
                return NotFound();
            }

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"DELETE api/food/{id} - Food item with ID: {id} deleted successfully");

            return NoContent();
        }
    }
}
