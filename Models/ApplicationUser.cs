using Microsoft.AspNetCore.Identity;

namespace PROG_PART_2.Models
{
    // Extend IdentityUser to include additional properties for the application user
    public class ApplicationUser : IdentityUser
    {
        // Property to store the user's first name
        public string Firstname { get; set; }

        // Property to store the user's last name
        public string Lastname { get; set; }
    }
}
