using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FootballAPI.Models
{
    public class Team
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Liga { get; set; } = null!;
    }
}
