using Microsoft.AspNetCore.Identity;

namespace PROJECT_S19.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
