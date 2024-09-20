using MongoDB.Driver;
using RecommendationList.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Charger la configuration MongoDB depuis appsettings.json
var mongoSettings = builder.Configuration.GetSection("MongoDB");

// Ajouter MongoDB en tant que service (enregistré comme singleton ici)
builder.Services.AddSingleton<IMongoClient>(s =>
{
    var connectionString = mongoSettings["ConnectionString"];
    return new MongoClient(connectionString);
});

builder.Services.AddSingleton(s =>
{
    var client = s.GetRequiredService<IMongoClient>();
    var databaseName = mongoSettings["DatabaseName"];
    return client.GetDatabase(databaseName);
});

// Ajouter les services CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200", "http://localhost:7222", "https://sallezen.com/") // Remplace avec l'URL de ton application Angular
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Facultatif, si tu veux autoriser les cookies ou l'authentification
        });
});

// Enregistrer RecommendationService comme Singleton
builder.Services.AddSingleton<RecommendationService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
