using NutriTrackMVCApp.Data;  // namespace 
using NutriTrackMVCApp.Models;  // Inkluderer modellen 'Food'
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutriTrackMVCApp.Controllers  // namespace for kontrolleren
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Konstruktør som tar inn ApplicationDbContext for å få tilgang til databasen.
        public FoodController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/food
        // Henter alle matvarer fra databasen.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetAllFoods()
        {
            // Returnerer en liste over alle matvarer asynkron.
            return await _context.Foods.ToListAsync();
        }

        // GET: api/food/{id}
        // Henter en spesifikk matvare basert på ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            // Søker etter matvaren med den spesifikke ID-en.
            var food = await _context.Foods.FindAsync(id);

            // Returnerer 404 Not Found hvis matvaren ikke finnes.
            if (food == null)
            {
                return NotFound();
            }

            // Returnerer matvaren hvis den finnes.
            return food;
        }

        // POST: api/food
        // Oppretter en ny matvare i databasen.
        [HttpPost]
        public async Task<ActionResult<Food>> CreateFood(Food food)
        {
            // Legger til den nye matvaren i databasen.
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();

            // Returnerer en 201 Created-respons med URL-en til den nye matvaren.
            return CreatedAtAction(nameof(GetFood), new { id = food.Id }, food);
        }

        // PUT: api/food/{id}
        // Oppdaterer en eksisterende matvare basert på ID.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFood(int id, Food food)
        {
            // Sjekker om ID-en i URL-en samsvarer med ID-en i JSON-bodyen.
            if (id != food.Id)
            {
                // Returnerer 400 Bad Request hvis ID-ene ikke samsvarer.
                return BadRequest("Id in URL does not match Id in body.");
            }

            // Marker matvaren som modifisert i konteksten slik at den kan oppdateres.
            _context.Entry(food).State = EntityState.Modified;

            try
            {
                // Prøver å lagre endringene asynkront.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Hvis matvaren ikke finnes i databasen, returnerer 404 Not Found.
                if (!_context.Foods.Any(e => e.Id == id))
                {
                    return NotFound("Food item not found.");
                }
                else
                {
                    // Hvis det oppstår andre problemer, kastes feilen videre.
                    throw;
                }
            }

            // Returnerer 204 No Content for å indikere at oppdateringen var vellykket.
            return NoContent();
        }

        // DELETE: api/food/{id}
        // Sletter en matvare basert på ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            // Søker etter matvaren med den spesifikke ID-en.
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                // Returnerer 404 Not Found hvis matvaren ikke finnes.
                return NotFound();
            }

            // Fjerner matvaren fra databasen.
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();

            // Returnerer 204 No Content for å indikere at slettingen var vellykket.
            return NoContent();
        }
    }
}
