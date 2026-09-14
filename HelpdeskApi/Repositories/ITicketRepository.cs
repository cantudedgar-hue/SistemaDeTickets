using HelpdeskApi.Models;

namespace HelpdeskApi.Repositories
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync();
        Task<Ticket?> GetByIdAsync(int id);
        Task<Ticket> CreateAsync(Ticket ticket);
        Task<bool> UpdateStatusAsync(int id, string newStatus, int changedByUserId);
        Task<bool> DeleteAsync(int id);
    }
}