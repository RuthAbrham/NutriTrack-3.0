using NutriTrackMVCApp.Data;  // namespace for Data-laget
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Konfigurer database-tilkoblingen.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// Konfigurer mellomvaren for applikasjonen.
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Food}/{action=Index}/{id?}");

app.Run();
