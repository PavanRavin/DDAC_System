using DDAC_System.Models;
using Microsoft.EntityFrameworkCore;

namespace DDAC_System.Models
{
    public class System_ClassDBContext: DbContext
    {
        public System_ClassDBContext(DbContextOptions<System_ClassDBContext> options)
            : base(options)
        {
        }
        public DbSet<AcademicClass> AcademicClass { get; set; }
    }
}
