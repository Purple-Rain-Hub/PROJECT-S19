using System.ComponentModel.DataAnnotations;

namespace PROJECT_S19.DTOs.Event
{
    public class UpdateEventResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
