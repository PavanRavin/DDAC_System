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

        public DbSet<Assignment> Assignment { get; set; }

        public DbSet<AssignmentSubmission> AssignmentSubmission { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>()
                .HasOne(e => e.AcademicClass)
                .WithMany(e => e.Assignment)
                .HasForeignKey(e => e.Class_Name)
                .HasPrincipalKey(e => e.Class_Name)
                .IsRequired();

            modelBuilder.Entity<AssignmentSubmission>()
                .HasOne(e => e.Assignment)
                .WithMany(e => e.AssignmentSubmission)
                .HasForeignKey(e => e.AssignmentID)
                .HasPrincipalKey(e => e.AssignmentID)
                .IsRequired();
        }
    }
}
