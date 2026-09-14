using HelpdeskApi.DTOs;

namespace HelpdeskApi.Services
{
    public interface ITicketService
    {
        Task<List<TicketDto>> GetAllTicketsAsync();
        Task<TicketDto?> GetTicketByIdAsync(int id);
        Task<TicketDto> CreateTicketAsync(CreateTicketDto dto);
        Task<bool> UpdateStatusAsync(int id, UpdateTicketStatusDto dto);
        Task<bool> DeleteTicketAsync(int id);
    }
}