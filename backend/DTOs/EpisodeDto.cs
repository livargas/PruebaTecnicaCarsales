using System.Text.Json.Serialization;

namespace PruebaTecnicaCarsales.DTOs
{
    public class EpisodeDto
    {

        public int Id { get; set; }
        public required string Name { get; set; }
        [JsonPropertyName("air_date")]
        public required string AirDate { get; set; }
        [JsonPropertyName("episode")]
        public required string EpisodeCode { get; set; }
        public List<string>? Characters { get; set; }
        public required string Url { get; set; }
        [JsonPropertyName("created")]
        public required string CreatedAt { get; set; }
        public List<CharacterDto>? fullCharacters { get; set; } = [];
    }
}
