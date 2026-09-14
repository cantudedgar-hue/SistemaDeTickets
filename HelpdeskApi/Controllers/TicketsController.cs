using Microsoft.AspNetCore.Mvc;
using HelpdeskApi.DTOs;
using HelpdeskApi.Services;

namespace HelpdeskApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _service;

        public TicketsController(ITicketService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TicketDto>>> GetAll()
        {
            return Ok(await _service.GetAllTicketsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetById(int id)
        {
            var ticket = await _service.GetTicketByIdAsync(id);
            return ticket == null ? NotFound() : Ok(ticket);
        }

        [HttpPost]
        public async Task<ActionResult<TicketDto>> Create(CreateTicketDto dto)
        {
            var created = await _service.CreateTicketAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateTicketStatusDto dto)
        {
            try
            {
                var updated = await _service.UpdateStatusAsync(id, dto);
                return updated ? NoContent() : NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteTicketAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}