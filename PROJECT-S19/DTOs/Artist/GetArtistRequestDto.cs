using PROJECT_S19.DTOs.Event;
using PROJECT_S19.Models;
using System.ComponentModel.DataAnnotations;

namespace PROJECT_S19.DTOs.Artist
{
    public class GetArtistRequestDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }   
        public required string Genre { get; set; } 
        public required string Bio { get; set; }
        public List<GetEventDto>? Events { get; set; }
    }
}
