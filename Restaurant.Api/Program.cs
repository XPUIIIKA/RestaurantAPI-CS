using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Extensions;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Services;
using Restaurant.Domain.IRepositories;
using Restaurant.Infrastructure.Persistence;
using Restaurant.Infrastructure.Repositories;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        path: "Logs/log.txt",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7
    )
    .Enrich.FromLogContext()
    .MinimumLevel.Debug()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    
    services.AddSerilog();
    
    services.AddDbContext<RestaurantDbContext>(options => 
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    
    services.AddScoped<IMenuService, MenuService>();
    services.AddScoped<IAnalyticsService, AnalyticsService>();
    services.AddScoped<ICheckService, CheckService>();

    services.AddScoped<ICheckRepository, CheckRepository>();
    services.AddScoped<IDishRepository, DishRepository>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    services.AddControllers();

    if (builder.Environment.IsDevelopment())
        services.AddSwaggerDocumentation();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseSwaggerDocumentation();



app.MapControllers();

app.Run();