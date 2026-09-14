namespace HelpdeskApi.DTOs
{
	public class TicketDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string Status { get; set; } = string.Empty;
		public string Priority { get; set; } = string.Empty;
		public string? UserName { get; set; }
		public string? AgentName { get; set; }
		public string? CategoryName { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}