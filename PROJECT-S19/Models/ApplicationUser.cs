using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PROJECT_S19.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        public ICollection<ApplicationUserRole>? ApplicationUserRoles { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
