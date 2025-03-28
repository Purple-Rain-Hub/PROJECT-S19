using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROJECT_S19.DTOs.Event;
using PROJECT_S19.DTOs.Ticket;
using PROJECT_S19.Services;

namespace PROJECT_S19.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User, Admin")]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto createTicketDto)
        {
            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var email = user.Value;

                var success = await _ticketService.CreateTicketAsync(createTicketDto, email);
                if (success)
                {
                    return Ok(new { message = "Ticket successfully purchased!" });
                }
                else
                {
                    return BadRequest(new { message = "Ticket is already purchased or something went wrong." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var email = user.Value;

                var result = await _ticketService.DeleteTicketAsync(id, email);

                return result ? Ok(new UpdateTicketResponseDto() { Message = "Ticket deleted" })
                    : BadRequest(new UpdateTicketResponseDto() { Message = "Something went wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var email = user.Value;

                var tickets = await _ticketService.GetTicketsAsync(email);

                if (tickets == null)
                {
                    return BadRequest(new
                    {
                        message = "Something went wrong"
                    });
                }

                var count = tickets.Count();

                var text = count == 1 ? $"{count} ticket found" : $"{count} tickets found";

                return Ok(new
                GetTicketResponseDto()
                {
                    Message = text,
                    Tickets = tickets
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            try
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var email = user.Value;

                var result = await _ticketService.GetTicketByIdAsync(id, email);

                return result != null ? Ok(new { message = "Ticket found", Ticket = result })
                    : BadRequest(new { message = "Something went wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
