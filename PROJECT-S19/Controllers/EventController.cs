using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROJECT_S19.DTOs.Event;
using PROJECT_S19.Services;

namespace PROJECT_S19.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto createEventDto)
        {
            try
            {
                var success = await _eventService.CreateEventAsync(createEventDto);
                if (success)
                {
                    return Ok(new { message = "Event successfully registered!" });
                }
                else
                {
                    return BadRequest(new { message = "Event is already registered or something went wrong." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEvent([FromQuery] int id, [FromBody] UpdateEventDto updateEvent)
        {
            try
            {
                var result = await _eventService.UpdateEventAsync(id, updateEvent);

                return result ? Ok(new UpdateEventResponseDto() { Message = "Event updated" })
                    : BadRequest(new UpdateEventResponseDto() { Message = "Something went wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                var result = await _eventService.DeleteEventAsync(id);

                return result ? Ok(new UpdateEventResponseDto() { Message = "Event deleted" })
                    : BadRequest(new UpdateEventResponseDto() { Message = "Something went wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                var events = await _eventService.GetEventsAsync();

                if (events == null)
                {
                    return BadRequest(new
                    {
                        message = "Something went wrong"
                    });
                }

                var count = events.Count();

                var text = count == 1 ? $"{count} event found" : $"{count} events found";

                return Ok(new
                GetEventResponseDto()
                {
                    Message = text,
                    Events = events
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            try
            {
                var result = await _eventService.GetEventByIdAsync(id);

                return result != null ? Ok(new { message = "Event found", Event = result })
                    : BadRequest(new { message = "Something went wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
