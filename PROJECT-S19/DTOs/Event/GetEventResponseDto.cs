using PROJECT_S19.DTOs.Artist;

namespace PROJECT_S19.DTOs.Event
{
    public class GetEventResponseDto
    {
        public required string Message { get; set; }
        public required List<GetEventRequestDto>? Events { get; set; }
    }
}
