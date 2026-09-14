namespace HelpdeskApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; 
        public DateTime CreatedAt { get; set; }

        // Relaciones de navegación
        public ICollection<Ticket>? TicketsCreados { get; set; }
        public ICollection<Ticket>? TicketsAsignados { get; set; }
    }
}
