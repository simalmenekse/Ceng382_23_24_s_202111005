namespace RazorPages.WebApp.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
