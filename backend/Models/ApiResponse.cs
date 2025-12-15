namespace PruebaTecnicaCarsales.Models
{
    public class ApiResponse<T>
    {
        public Info Info { get; set; } = new();
        public List<T> Results { get; set; } = [];
    }
}
