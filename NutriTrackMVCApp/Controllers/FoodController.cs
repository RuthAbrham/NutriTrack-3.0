using NutriTrackMVCApp.Data;  // namespace 
using NutriTrackMVCApp.Models;  // Inkluderer modellen 'Food'
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutriTrackMVCApp.Controllers  // namespace for kontrolleren
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Food/Create
        // Handle form submission to create a new food item
        [HttpPost]
        [ValidateAntiForgeryToken]
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
