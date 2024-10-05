using Microsoft.EntityFrameworkCore;
using RecommendationList.Model;

namespace RecommendationList.Data;

public class RecommendationsListsDbContext : DbContext
{
    public RecommendationsListsDbContext(DbContextOptions<RecommendationsListsDbContext> options) : base(options) { }

    public DbSet<Recommendation> Recommendations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recommendation>().ToTable("Recommendations").HasKey(g => g.Id); // Clé primaire
    }
}