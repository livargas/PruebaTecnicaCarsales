namespace PruebaTecnicaCarsales.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string AirDate { get; set; }
        public required string EpisodeCode { get; set; }
    }
}
