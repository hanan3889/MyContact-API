using MyContact_API;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajout du DbContext
builder.Services.AddDbContext<MyContactDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

// Ajout des contrôleurs
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true; // Active l'indentation
});

// Permet la génération automatique de documentation des routes
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("V0.1", new OpenApiInfo { Title = "MyContact API", Description = "Donne accès à la base de données ", Version = "v0.1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/V0.1/swagger.json", "MyContact API V0.1");
    });
}

app.UseRouting();
app.MapControllers();

// Possible d'ajouter CORS, regarder documentation ASP.NET CORE
// builder.Services.AddCors(options => {});

app.MapGet("/", () => "Salem alikoum!");

app.Run();
