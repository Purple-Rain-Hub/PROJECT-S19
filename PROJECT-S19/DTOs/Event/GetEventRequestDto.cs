using System.ComponentModel.DataAnnotations;

namespace PROJECT_S19.DTOs.Event
{
    public class GetEventRequestDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required DateTime Date { get; set; }
        public required string Location { get; set; }
        public required int ArtistId { get; set; }
    }
}
