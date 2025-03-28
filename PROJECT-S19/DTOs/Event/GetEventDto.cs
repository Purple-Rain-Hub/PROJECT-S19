using System.ComponentModel.DataAnnotations;

namespace PROJECT_S19.DTOs.Event
{
    public class GetEventDto
    {
        public required string Title { get; set; }
        public required DateTime Date { get; set; }
        public required string Location { get; set; }
    }
}
