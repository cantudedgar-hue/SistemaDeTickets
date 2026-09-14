using Microsoft.EntityFrameworkCore;
using HelpdeskApi.Models;

namespace HelpdeskApi.Data
{
    public class HelpdeskContext : DbContext
    {
        public HelpdeskContext(DbContextOptions<HelpdeskContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketHistory> TicketHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeo a los nombres de tabla en minúsculas que ya creaste en Postgres
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Ticket>().ToTable("tickets");
            modelBuilder.Entity<TicketHistory>().ToTable("ticket_history");

            // Ticket  User quien lo creó
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany(u => u.TicketsCreados)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket  User agente asignado
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.AssignedAgent)
                .WithMany(u => u.TicketsAsignados)
                .HasForeignKey(t => t.AssignedAgentId)
                .OnDelete(DeleteBehavior.Restrict);

            // TicketHistory  User quien hizo el cambio
            modelBuilder.Entity<TicketHistory>()
                .HasOne(th => th.ChangedByUser)
                .WithMany()
                .HasForeignKey(th => th.ChangedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
