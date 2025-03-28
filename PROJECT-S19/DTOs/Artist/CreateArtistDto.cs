using System.ComponentModel.DataAnnotations;

namespace PROJECT_S19.DTOs.Artist
{
    public class CreateArtistDto
    {
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Genre { get; set; }
        [Required]
        [MaxLength(1000)]
        public required string Bio { get; set; }
    }
}
