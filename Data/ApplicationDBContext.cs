using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PROG_PART_2.Models;

namespace PROG_PART_2.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {
       
            public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
            {
            }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Claim> Claims { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
