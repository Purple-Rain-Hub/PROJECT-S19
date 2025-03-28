using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROJECT_S19.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Title { get; set; }
        [Required]
        public required DateTime Date { get; set; }
        [Required]
        [StringLength(50)]
        public required string Location { get; set; }
        [Required]
        public required int ArtistId { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }

        [ForeignKey("ArtistId")]
        public Artist Artist { get; set; }
    }
}
