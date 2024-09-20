using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecommendationList.Model
{
    // Indique à MongoDB que cette classe est un document
    [BsonIgnoreExtraElements]
    public class Recommendation
    {
        // Enum pour les catégories de recommandation
        [JsonConverter(typeof(JsonStringEnumConverter))] // Sérialise et désérialise en tant que chaîne dans JSON
        public enum Categories
        {
            Movie,
            VideoGame,
            TVshow,
            Music,
            YoutubeVideo,
            Book
        }

        // ID du document, représenté comme un ObjectId dans MongoDB
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Indique que cet ID sera un ObjectId
        public string id { get; set; } // Utilisation de la convention PascalCase pour les propriétés publiques

        // Nom de la recommandation
        [Required]
        [BsonElement("name")] // Optionnel : Pour explicitement définir le champ dans MongoDB
        public string Name { get; set; }

        // Auteur de la recommandation
        [BsonElement("author")]
        public string Author { get; set; }

        // Description de la recommandation
        [BsonElement("description")]
        public string Description { get; set; }

        // Date de la recommandation
        [BsonElement("date")]
        public DateTime Date { get; set; }

        // Catégorie, enregistrée sous forme de chaîne dans MongoDB
        [BsonElement("category")]
        [BsonRepresentation(BsonType.String)] // Enregistre l'énumération comme une chaîne dans MongoDB
        public Categories category { get; set; }

        // Constructeur par défaut
        public Recommendation() { }

        // Constructeur avec paramètres
        public Recommendation(string id, string name, string author, string description, DateTime date, Categories category)
        {
            this.id = id;
            this.Name = name;
            this.Author = author;
            this.Description = description;
            this.Date = date;
            this.category = category;
        }
    }
}
