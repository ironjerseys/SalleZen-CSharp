using Microsoft.EntityFrameworkCore;
using RecommendationList.Data;
using RecommendationList.Model;
using System.Text.Json;

namespace RecommendationList.Service;
public class RecommendationService
{
    private readonly RecommendationsListsDbContext _context;
    private readonly Serilog.ILogger _logger;

    public RecommendationService(Serilog.ILogger logger, RecommendationsListsDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Récupère toutes les recommandations
    public async Task<List<Recommendation>> GetAllAsync()
    {
        List<Recommendation> result = new List<Recommendation>();
        try
        {
            result = await _context.Recommendations.ToListAsync();
            _logger.Information("{MethodName} - Number of objects returned {Result}", nameof(GetAllAsync), result.Count());
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error("{MethodName} - {Exception}", nameof(GetAllAsync), ex);
            throw new Exception();
        }
    }

    // Récupère une recommandation par ID
    public async Task<Recommendation> GetByIdAsync(int id)
    {
        Recommendation? result = new Recommendation();
        try
        {
            result = await _context.Recommendations.FindAsync(id);
            if (result != null)
            {
                _logger.Information("{MethodName} - Recommendation saved in database : {Recommendation}", nameof(GetByIdAsync), JsonSerializer.Serialize(result));
                return result;
            }
            return null;
        }
        catch (Exception ex)
        {
            _logger.Error("{MethodName} - {Exception}", nameof(GetByIdAsync), ex);
            throw new Exception();
        }
    }

    // Crée une nouvelle recommandation
    public async Task CreateAsync(Recommendation recommendation)
    {
        try
        {
            await _context.Recommendations.AddAsync(recommendation);
            await _context.SaveChangesAsync();
            _logger.Information("{MethodName} - Recommendation saved in database : {Recommendation}", nameof(CreateAsync), JsonSerializer.Serialize(recommendation));
        }
        catch (Exception ex)
        {
            _logger.Error("{MethodName} - {Exception}", nameof(CreateAsync), ex);
            throw new Exception();
        }
    }

    // Met à jour une recommandation existante
    public async Task UpdateAsync(int id, Recommendation recommendationIn)
    {
        try
        {
            var recommendation = await _context.Recommendations.FindAsync(id);
            if (recommendation != null)
            {
                _context.Entry(recommendation).CurrentValues.SetValues(recommendationIn);
                await _context.SaveChangesAsync();
                _logger.Information("{MethodName} - Recommendation updated : {Recommendation}", nameof(UpdateAsync), JsonSerializer.Serialize(recommendation));
            }
        }
        catch (Exception ex)
        {
            _logger.Error("{MethodName} - {Exception}", nameof(UpdateAsync), ex);
            throw new Exception();
        }
    }

    // Supprime une recommandation par ID
    public async Task DeleteAsync(int id)
    {
        try
        {
            var recommendation = await _context.Recommendations.FindAsync(id);
            if (recommendation != null)
            {
                _context.Recommendations.Remove(recommendation);
                await _context.SaveChangesAsync();
                _logger.Information("{MethodName} - Recommendation removed : {Recommendation}", nameof(DeleteAsync), JsonSerializer.Serialize(recommendation));

            }
        }
        catch (Exception ex)
        {
            _logger.Error("{MethodName} - {Exception}", nameof(DeleteAsync), ex);
            throw new Exception();
        }
    }
}
