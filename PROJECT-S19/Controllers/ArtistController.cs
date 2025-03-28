using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROJECT_S19.DTOs.Account;
using PROJECT_S19.DTOs.Artist;
using PROJECT_S19.Services;

namespace PROJECT_S19.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ArtistService _artistService;

        public ArtistController(ArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateArtist([FromBody] CreateArtistDto createArtistDto)
        {
            var success = await _artistService.CreateArtistAsync(createArtistDto);
            if (success)
            {
                return Ok(new { message = "Artist successfully registered!" });
            }
            else
            {
                return BadRequest(new { message = "Artist is already registered or something went wrong." });
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateArtist([FromQuery] int id, [FromBody] CreateArtistDto updateArtist)
        {
            try
            {
                var result = await _artistService.UpdateArtistAsync(id, updateArtist);

                return result ? Ok(new UpdateArtistResponseDto() { Message = "Artist updated" })
                    : BadRequest(new UpdateArtistResponseDto() { Message = "Something went wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            try
            {
                var result = await _artistService.DeleteArtistAsync(id);

                return result ? Ok(new UpdateArtistResponseDto() { Message = "Artist deleted" })
                    : BadRequest(new UpdateArtistResponseDto() { Message = "Something went wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetArtists()
        {
            try
            {
                var artists = await _artistService.GetArtistsAsync();

                if (artists == null)
                {
                    return BadRequest(new
                    {
                        message = "Something went wrong"
                    });
                }

                var count = artists.Count();

                var text = count == 1 ? $"{count} artist found" : $"{count} artists found";

                return Ok(new
                GetArtistResponseDto()
                {
                    Message = text,
                    Artists = artists
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            try
            {
                var result = await _artistService.GetArtistByIdAsync(id);

                return result != null ? Ok(new { message = "Artist found", Artist = result })
                    : BadRequest(new { message = "Something went wrong" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
