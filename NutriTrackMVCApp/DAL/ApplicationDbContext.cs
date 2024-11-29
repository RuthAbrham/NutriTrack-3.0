using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutriTrackMVCApp.Models; // Namespace for models

namespace NutriTrackMVCApp.Data // Namespace
{
    // The ApplicationDbContext inherits from IdentityDbContext to integrate Identity.
    public class ApplicationDbContext : IdentityDbContext
    {
        // Define a DbSet for your custom Food model.
        public DbSet<Food> Foods { get; set; }

        // Constructor to accept DbContext options and pass them to the base class.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Override OnModelCreating to apply additional configurations if needed.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add any specific configurations for your models here (optional).
           
            // Specify precision for the Price column in the Food table
            modelBuilder.Entity<Food>()
             .Property(f => f.Price)
             .HasPrecision(18, 2); // 18 digits in total, 2 after the decimal point
            
        }
    }
}

    

