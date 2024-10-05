using Microsoft.EntityFrameworkCore;
using RecommendationList.Data;
using RecommendationList.Model;

namespace RecommendationList.Service;
public class RecommendationService
{
    private readonly RecommendationsListsDbContext _context;

    public RecommendationService(RecommendationsListsDbContext context)
    {
        _context = context;
    }

    // Récupère toutes les recommandations
    public async Task<List<Recommendation>> GetAllAsync() =>
        await _context.Recommendations.ToListAsync();  

    // Récupère une recommandation par ID
    public async Task<Recommendation> GetByIdAsync(Guid id) =>
        await _context.Recommendations.FindAsync(id); 

    // Crée une nouvelle recommandation
    public async Task CreateAsync(Recommendation recommendation)
    {
        await _context.Recommendations.AddAsync(recommendation);  
        await _context.SaveChangesAsync();  
    }

    // Met à jour une recommandation existante
    public async Task UpdateAsync(Guid id, Recommendation recommendationIn)
    {
        var recommendation = await _context.Recommendations.FindAsync(id);
        if (recommendation != null)
        {
            _context.Entry(recommendation).CurrentValues.SetValues(recommendationIn);
            await _context.SaveChangesAsync(); 
        }
    }

    // Supprime une recommandation par ID
    public async Task DeleteAsync(Guid id)
    {
        var recommendation = await _context.Recommendations.FindAsync(id);
        if (recommendation != null)
        {
            _context.Recommendations.Remove(recommendation);  
            await _context.SaveChangesAsync();  
        }
    }
}
