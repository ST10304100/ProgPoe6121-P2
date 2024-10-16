using Microsoft.AspNetCore.Identity;

namespace PROG_PART_2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
