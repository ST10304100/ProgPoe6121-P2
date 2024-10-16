using Microsoft.EntityFrameworkCore;
using PROG_PART_2.Models;

namespace PROG_PART_2.Data
{
    public class ApplicationDBContext : DbContext
    {
       
            public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
            {
            }

        public DbSet<Document> Documents { get; set; }

    }
}
