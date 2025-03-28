using System.ComponentModel.DataAnnotations;

namespace PROJECT_S19.DTOs.Ticket
{
    public class UpdateTicketResponseDto
    {
        [Required]
        public required string Message { get; set; }
    }
}
