using HelpdeskApi.DTOs;
using HelpdeskApi.Models;
using HelpdeskApi.Repositories;

namespace HelpdeskApi.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repository;

        private static readonly string[] ValidStatuses = { "abierto", "en_progreso", "cerrado" };

        public TicketService(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TicketDto>> GetAllTicketsAsync()
        {
            var tickets = await _repository.GetAllAsync();
            return tickets.Select(MapToDto).ToList();
        }

        public async Task<TicketDto?> GetTicketByIdAsync(int id)
        {
            var ticket = await _repository.GetByIdAsync(id);
            return ticket == null ? null : MapToDto(ticket);
        }

        public async Task<TicketDto> CreateTicketAsync(CreateTicketDto dto)
        {
            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                UserId = dto.UserId,
                CategoryId = dto.CategoryId,
                Status = "abierto",
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repository.CreateAsync(ticket);
            var full = await _repository.GetByIdAsync(created.Id);
            return MapToDto(full!);
        }

        public async Task<bool> UpdateStatusAsync(int id, UpdateTicketStatusDto dto)
        {
            // Regla de negocio: solo se aceptan estos 3 estados
            if (!ValidStatuses.Contains(dto.NewStatus))
                throw new ArgumentException($"Status inválido: {dto.NewStatus}");

            return await _repository.UpdateStatusAsync(id, dto.NewStatus, dto.ChangedByUserId);
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static TicketDto MapToDto(Ticket t) => new()
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Status = t.Status,
            Priority = t.Priority,
            UserName = t.User?.Name,
            AgentName = t.AssignedAgent?.Name,
            CategoryName = t.Category?.Name,
            CreatedAt = t.CreatedAt
        };
    }
}