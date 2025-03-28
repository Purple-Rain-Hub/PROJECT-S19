using System.ComponentModel.DataAnnotations;

namespace PROJECT_S19.DTOs.Artist
{
    public class GetArtistResponseDto
    {
        public required string Message { get; set; }
        public required List<GetArtistRequestDto>? Artists { get; set; }
    }
}
