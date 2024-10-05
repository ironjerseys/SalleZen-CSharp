using Microsoft.EntityFrameworkCore;
using RecommendationList.Data;
using RecommendationList.Service;

var builder = WebApplication.CreateBuilder(args);

// Service de base de données SQL Server
builder.Services.AddDbContext<RecommendationsListsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RecommendationsListsDbConnection")));

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

// Enregistrer RecommendationService comme Singleton
builder.Services.AddScoped<RecommendationService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAngularApp");

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