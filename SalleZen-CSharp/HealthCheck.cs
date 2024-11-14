using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RecommendationList.Data;

namespace SalleZen_CSharp;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly RecommendationsListsDbContext _dbContext;

    public DatabaseHealthCheck(RecommendationsListsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // Tentative d'accès à la base de données
            await _dbContext.Database.ExecuteSqlRawAsync("SELECT 1", cancellationToken);
            return HealthCheckResult.Healthy("Database connection is healthy.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Database connection is unhealthy.", ex);
        }
    }
}
