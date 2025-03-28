using System.ComponentModel.DataAnnotations;

namespace PROJECT_S19.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Genre { get; set; }
        [Required]
        [MaxLength(1000)]
        public required string Bio { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
