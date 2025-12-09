using api.Models;
using FinShark.Data;
using FinShark.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. This is .NET's Dependency Injection container
//(like Provider or GetIt in Flutter). You register services here that can be injected throughout
//your app.This is .NET's Dependency Injection container (like Provider or GetIt in Flutter). 
//You register services here that can be injected throughout your app.

// This line adds support for controllers, which are classes that handle HTTP requests and return responses.
builder.Services.AddControllers();

// These two lines set up Swagger, a tool for generating interactive API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

builder.Services.AddScoped<IStockRepository, StockRepository>();

//This finalizes the configuration and creates the actual web application instance.
//Think of this as the point where Flutter builds your widget tree.
var app = builder.Build();

//Checks if you're running in development mode (similar to kDebugMode in Flutter).
//UseSwagger() & UseSwaggerUI(): Only in development, this enables the Swagger UI 
//at /swagger where you can test your API endpoints interactively.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//This middleware automatically redirects HTTP requests to HTTPS (for security).
//It's like enforcing secure connections.
app.UseHttpsRedirection();



// It tells ASP.NET Core to discover and wire up all your [ApiController] or Controller classes so their action methods become accessible HTTP endpoints.
app.MapControllers();

app.Run();

