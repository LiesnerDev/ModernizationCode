using Microsoft.EntityFrameworkCore;
using Employee.Domain.Entities;

namespace Employee.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeId");
                entity.Property(e => e.Name).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Age).IsRequired();
                entity.Property(e => e.Address).HasMaxLength(30).IsRequired();
            });
        }
    }
}