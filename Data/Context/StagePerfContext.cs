using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Context
{
    public class StagePerfContext : DbContext
    {
        public StagePerfContext(DbContextOptions<StagePerfContext> options) : base(options)
        {     
        }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>().Property<int>(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Ticket>().HasKey(t => t.Id);
            modelBuilder.Entity<Employee>().Property<int>(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Employee)
                .WithMany(e => e.Tickets)
                .HasForeignKey(t => t.EmployeeId);
        }
    }
}
