namespace GuidoAloise.Api.Models
{
    public class NewsItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Date { get; set; } = string.Empty;
        public DateTime PointerDate { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Reference { get; set; } // URL esterno
    }
}
