using Microsoft.EntityFrameworkCore;
using NutriTrackMVCApp.Models;  //  namespace for modellene

namespace NutriTrackMVCApp.Data  // namespace
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Food> Foods { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Legg til eventuelle spesifikke konfigurasjoner her.
        }
    }
}
