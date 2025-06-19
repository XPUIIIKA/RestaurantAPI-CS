using Microsoft.EntityFrameworkCore;
using Restaurant.Infrastructure;
using Restaurant.Infrastructure.Persistence;
using Serilog;
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(new JsonFormatter())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSerilog();
    
    builder.Services.AddDbContext<RestaurantDbContext>(options => 
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
}


var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();