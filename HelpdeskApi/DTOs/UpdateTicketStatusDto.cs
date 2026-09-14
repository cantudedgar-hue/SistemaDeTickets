namespace HelpdeskApi.DTOs
{
    public class UpdateTicketStatusDto
    {
        public string NewStatus { get; set; } = string.Empty;
        public int ChangedByUserId { get; set; }
    }
}