using System.Text.Json.Serialization;

namespace PruebaTecnicaCarsales.DTOs
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Status { get; set; }
        public required string Species { get; set; }
        public required string Type { get; set; }
        public required string Gender { get; set; }
        public required Origin Origin { get; set; }
        public required Location Location { get; set; }
        public required string Image { get; set; }
        [JsonPropertyName("episode")]
        public List<string> Episodes { get; set; } = [];
        public required string Url { get; set; }
        [JsonPropertyName("created")]
        public required string CreatedAt { get; set; }


    }

    public class Origin
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
    }

    public class Location
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
    }
}