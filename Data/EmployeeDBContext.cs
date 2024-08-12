using EmployeeInfoManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInfoManagement.Data
{
    public class EmployeeDBContext : DbContext
    {

        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Phone)
            .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();
        }
    }
}
