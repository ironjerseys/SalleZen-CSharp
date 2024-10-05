using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RecommendationList.Model;

public class Recommendation
{
    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public enum Categories
    {
        Movie,
        VideoGame,
        TVshow,
        Music,
        YoutubeVideo,
        Book
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } 

    [Required]
    public string Name { get; set; }

    public string Author { get; set; }

    public string Description { get; set; }

    public DateTime RecommendationDate { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public Categories category { get; set; }

    public Recommendation() { }

    public Recommendation(int id, string name, string author, string description, DateTime date, Categories category)
    {
        this.Id = id;
        this.Name = name;
        this.Author = author;
        this.Description = description;
        this.RecommendationDate = date;
        this.category = category;
    }
}
