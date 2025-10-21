var builder = WebApplication.CreateBuilder(args);

// Add services to the container. This is .NET's Dependency Injection container
//(like Provider or GetIt in Flutter). You register services here that can be injected throughout
//your app.This is .NET's Dependency Injection container (like Provider or GetIt in Flutter). 
//You register services here that can be injected throughout your app.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

//runApp(MyApp()) in flutter
app.Run();

//A record in C# is like a data class or freezed class in Dart. It's immutable by default and
//perfect for DTOs (Data Transfer Objects).
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
