using NutriTrackMVCApp.Data;  // namespace for Data-laget
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NutriTrackMVCApp.Services; // Namespace for Services
using NutriTrackMVCApp.Repositories; // Replace with your repository namespace



var builder = WebApplication.CreateBuilder(args);

// Add console logging for debugging
builder.Logging.AddConsole();

// Konfigurer database-tilkoblingen.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Konfigurer Identity-tjenestene.
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>() // Legg til roller
    .AddEntityFrameworkStores<ApplicationDbContext>();



// Register the AuthorizationService
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
// Register the repository service (add it here).
builder.Services.AddScoped<IFoodRepository, FoodRepository>();

// Legg til Swagger-tjenestene.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Legg til MVC-st�tte.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Konfigurer Swagger-bruk i utviklingsmilj� og produksjon.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Ensure the database is created and seeded with initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Ensure the database is created and migrated.
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate(); // Ensures the database is created

    // Seed roles and admin user.
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    await DatabaseInitializer.SeedAsync(context, roleManager, userManager);
    
}

// Konfigurer mellomvaren for applikasjonen.
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Legg til autentisering
app.UseAuthorization();  // Legg til autorisasjon



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Food}/{action=Index}/{id?}");


app.MapRazorPages(); // Ensure Razor Pages are mapped

app.Run();
