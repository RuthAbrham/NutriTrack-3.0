using NutriTrackMVCApp.Data;  // Namespace for data layer
using NutriTrackMVCApp.Models;  // Namespace for models
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;  // Import for role-based authorization
using System.Linq;
using System.Threading.Tasks;

namespace NutriTrackMVCApp.Controllers  // Namespace for the controller
{
    public class FoodController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor for ApplicationDbContext to access the database
        public FoodController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Food
        // Display all food items (Index page)
        public async Task<IActionResult> Index()
        {
            var foods = await _context.Foods.ToListAsync();
            return View(foods);
        }

        // GET: /Food/Details/{id}
        // Display details of a specific food item
        public async Task<IActionResult> Details(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // GET: /Food/Create
        // Display form to create a new food item
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Food/Create
        // Handle form submission to create a new food item
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Food food)
        {
            if (ModelState.IsValid)
            {
                _context.Foods.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: /Food/Edit/{id}
        // Display form to edit an existing food item
        [Authorize(Roles = "Admin")]  // Restrict to Admins
        public async Task<IActionResult> Edit(int id)
        {
            var food = await _context.Foods.FindAsync(id);
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
        [Authorize(Roles = "Admin")]  // Restrict to Admins
        public async Task<IActionResult> Edit(int id, Food food)
        {
            if (id != food.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Foods.Any(e => e.Id == id))
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
        [Authorize(Roles = "Admin")]  // Restrict to Admins
        public async Task<IActionResult> Delete(int id)
        {
            var food = await _context.Foods.FindAsync(id);
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
        [Authorize(Roles = "Admin")]  // Restrict to Admins
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food != null)
            {
                _context.Foods.Remove(food);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
