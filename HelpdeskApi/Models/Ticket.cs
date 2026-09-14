namespace HelpdeskApi.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = "abierto";
        public string Priority { get; set; } = "media";

        public int UserId { get; set; }
        public User? User { get; set; }

        public int? AssignedAgentId { get; set; }
        public User? AssignedAgent { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public ICollection<TicketHistory>? History { get; set; }
    }
}