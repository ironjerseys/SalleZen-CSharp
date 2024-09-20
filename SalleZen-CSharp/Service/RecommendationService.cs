using MongoDB.Driver;
using RecommendationList.Model;

namespace RecommendationList.Service;

public class RecommendationService
{
    private readonly IMongoCollection<Recommendation> _recommendations;

    public RecommendationService(IMongoDatabase database)
    {
        _recommendations = database.GetCollection<Recommendation>("recommendation");
    }

    public async Task<List<Recommendation>> GetAllAsync() =>
        await _recommendations.Find(recommendation => true).ToListAsync();

    public async Task<Recommendation> GetByIdAsync(string id) =>
        await _recommendations.Find<Recommendation>(rec => rec.id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Recommendation recommendation) =>
        await _recommendations.InsertOneAsync(recommendation);

    public async Task UpdateAsync(string id, Recommendation recommendationIn) =>
        await _recommendations.ReplaceOneAsync(rec => rec.id == id, recommendationIn);

    public async Task DeleteAsync(string id) =>
        await _recommendations.DeleteOneAsync(rec => rec.id == id);
}

