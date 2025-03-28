using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROJECT_S19.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        [Required]
        public required int EventId { get; set; }
        [Required]
        public required string UserId { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
