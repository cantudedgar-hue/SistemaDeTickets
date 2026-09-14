using Microsoft.EntityFrameworkCore;
using HelpdeskApi.Data;
using HelpdeskApi.Models;

namespace HelpdeskApi.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly HelpdeskContext _context;

        public TicketRepository(HelpdeskContext context)
        {
            _context = context;
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _context.Tickets
                .Include(t => t.User)
                .Include(t => t.AssignedAgent)
                .Include(t => t.Category)
                .ToListAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            return await _context.Tickets
                .Include(t => t.User)
                .Include(t => t.AssignedAgent)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Ticket> CreateAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<bool> UpdateStatusAsync(int id, string newStatus, int changedByUserId)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return false;

            var oldStatus = ticket.Status;
            ticket.Status = newStatus;
            if (newStatus == "cerrado") ticket.ClosedAt = DateTime.UtcNow;

            _context.TicketHistories.Add(new TicketHistory
            {
                TicketId = id,
                ChangedBy = changedByUserId,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                ChangedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return false;

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}