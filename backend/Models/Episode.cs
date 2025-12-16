using System.Text.Json.Serialization;

namespace PruebaTecnicaCarsales.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [JsonPropertyName("air_date")]
        public required string AirDate { get; set; }

        [JsonPropertyName("episode")]
        public required string EpisodeCode { get; set; }

        public List<string> Characters { get; set; } = new();

        public required string Url { get; set; }

        [JsonPropertyName("created")]
        public required string CreatedAt { get; set; }
    }
}
