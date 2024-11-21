using Microsoft.AspNetCore.Identity;

namespace PROG_PART_2.Models
{
    // Extend IdentityUser to include additional properties for the application user
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
    }
}