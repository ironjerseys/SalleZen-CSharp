using Microsoft.EntityFrameworkCore;
using RecommendationList.Data;
using RecommendationList.Service;
using SalleZen_CSharp;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Service de base de données SQL Server
builder.Services.AddDbContext<RecommendationsListsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RecommendationsListsDbConnection")));

// Health Check 
builder.Services.AddHealthChecks().AddCheck<DatabaseHealthCheck>("Database");

// Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)  // Charger la config depuis appsettings.json
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Ajouter les services CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200", "http://localhost:7222", "https://sallezen.com")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

builder.Services.AddScoped<RecommendationService>();
builder.Services.AddSingleton(Log.Logger);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAngularApp");

// Active le point d'accès Health Check
app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();