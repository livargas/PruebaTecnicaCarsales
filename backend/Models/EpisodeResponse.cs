namespace PruebaTecnicaCarsales.Models
{
    public class EpisodeResponse<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<T> Episodes { get; set; } = Enumerable.Empty<T>();
    }
}
