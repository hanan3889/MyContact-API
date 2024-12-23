
using MyContact_API;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;


var builder = WebApplication.CreateBuilder(args);
//Ajout du DbContext
builder.Services.AddDbContext<MyContactDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

// ajout des controlleurs
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true; // Active l'indentation
});


//Permet la g�n�ration automatique de documentation des routes
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{   //le premier argument, en l'occurence "v0.1" d�termine le lien d�fini dans c.swaggerEndPoint()
    //Si on a "v1" ici et "/swagger/v01/swagger.json" dans SwaggerEndPoint(), il y aura une fetch error lorsqu'on essaye d'acc�der � la documentation
    c.SwaggerDoc("V0.1", new OpenApiInfo { Title = "MyContact API", Description = "Donne acc�s � la base de donn�es ", Version = "v0.1" });
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

//Possible d'ajouter CORS, regarder documentation ASP.NET CORE
//builder.Services.AddCors(options => {});

app.MapGet("/", () => "Hello World!");

app.Run();
