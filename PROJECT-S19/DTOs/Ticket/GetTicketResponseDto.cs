using PROJECT_S19.DTOs.Artist;
using PROJECT_S19.DTOs.Ticket;

namespace PROJECT_S19.DTOs.Event
{
    public class GetTicketResponseDto
    {
        public required string Message { get; set; }
        public required List<GetTicketRequestDto>? Tickets { get; set; }
    }
}
