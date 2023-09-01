using DDAC_System.Models;
using Microsoft.EntityFrameworkCore;

namespace DDAC_System.Models
{
    public class System_AssignmentDBContext : DbContext
    {
        public System_AssignmentDBContext(DbContextOptions<System_AssignmentDBContext> options)
            : base(options)
        {
        }
        public DbSet<Assignment> Assignment { get; set; }
    }
}
