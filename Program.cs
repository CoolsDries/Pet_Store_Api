using Microsoft.EntityFrameworkCore;
using Pet_Store_Api.Data;
using Pet_Store_Api.Models.Interfaces;
using Pet_Store_Api.Repositories;
using Pet_Store_Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Set CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()    // Allow all http methods (GET, POST, etc.)
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
// Datbase connection
// DefaultConnection located in appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PetStoreContext>(options => options.UseSqlServer(connectionString));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection 
builder.Services.AddScoped<DataInitializer>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<ISpeciesRepository, SpeciesRepository>();

builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IAnimalService, AnimalService>();

var app = builder.Build();

// Initialize data
using (var scope = app.Services.CreateScope())
{
    // service not found? GetRequiredService throws error | GetService returns null
    var context = scope.ServiceProvider.GetRequiredService<DataInitializer>();

    // Initialize data
    context.InitializeData();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Use CORS policy
    // Only in development, not safe for production
    app.UseCors("AllowAllOrigins");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();