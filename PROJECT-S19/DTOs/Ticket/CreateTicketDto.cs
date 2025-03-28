using System.ComponentModel.DataAnnotations;

namespace PROJECT_S19.DTOs.Ticket
{
    public class CreateTicketDto
    {
        [Required]
        public required int EventId { get; set; }
        [Required]
        public int Quantity { get; set; } = 1;
    }
}
