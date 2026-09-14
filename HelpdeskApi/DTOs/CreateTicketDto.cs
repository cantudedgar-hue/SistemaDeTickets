namespace HelpdeskApi.DTOs
{
    public class CreateTicketDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Priority { get; set; } = "media";
        public int UserId { get; set; }
        public int? CategoryId { get; set; }
    }
}