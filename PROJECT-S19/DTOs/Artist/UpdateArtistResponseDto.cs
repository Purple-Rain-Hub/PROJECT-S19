using System.ComponentModel.DataAnnotations;

namespace PROJECT_S19.DTOs.Artist
{
    public class UpdateArtistResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
