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
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and table settings here if necessary
            modelBuilder.Entity<Claim>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(u => u.Claims)
                .HasForeignKey(c => c.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Claim)
                .WithMany(c => c.Documents)
                .HasForeignKey(d => d.ClaimId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
