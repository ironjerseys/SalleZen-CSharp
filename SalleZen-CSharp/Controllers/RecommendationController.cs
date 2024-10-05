using Microsoft.AspNetCore.Mvc;
using RecommendationList.Model;
using RecommendationList.Service;

namespace RecommendationList.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecommendationController : ControllerBase
{
    private readonly RecommendationService _recommendationService;

    // Injecter le service RecommendationService dans le contrôleur
    public RecommendationController(RecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    // GET: api/recommendations
    [HttpGet]
    //[HttpGet("{id:guid}")]
    public async Task<ActionResult<IEnumerable<Recommendation>>> GetRecommendations()
    {
        var result = await _recommendationService.GetAllAsync();
        return Ok(result);
    }

    // GET: api/recommendations/{id}
    [HttpGet("{id:length(24)}", Name = "GetRecommendation")]
    public async Task<ActionResult<Recommendation>> GetRecommendationById(Guid id)
    {
        var recommendation = await _recommendationService.GetByIdAsync(id);

        if (recommendation == null)
        {
            return NotFound();
        }

        return Ok(recommendation);
    }

    // POST: api/recommendations
    [HttpPost]
    public async Task<ActionResult<Recommendation>> CreateRecommendation(Recommendation recommendation)
    {
        //recommendation.Id = null; 
        recommendation.RecommendationDate = DateTime.UtcNow;
        await _recommendationService.CreateAsync(recommendation);
        return CreatedAtRoute("GetRecommendation", new { id = recommendation.Id }, recommendation);
    }

    // PUT: api/recommendations/{id}
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateRecommendation(Guid id, Recommendation recommendationIn)
    {
        var recommendation = await _recommendationService.GetByIdAsync(id);

        if (recommendation == null)
        {
            return NotFound();
        }

        await _recommendationService.UpdateAsync(id, recommendationIn);
        return NoContent();
    }

    // DELETE: api/recommendations/{id}
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteRecommendation(Guid id)
    {
        var recommendation = await _recommendationService.GetByIdAsync(id);

        if (recommendation == null)
        {
            return NotFound();
        }

        await _recommendationService.DeleteAsync(id);
        return NoContent();
    }
}
