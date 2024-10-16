using Microsoft.EntityFrameworkCore;

namespace PROG_PART_2.Data
{
    public class ApplicationDBContext : DbContext
    {
       
            public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
            {
            }
           

    }
}
