using ExamProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
          public DbSet<Employee> Employees { get; set; }
        public DbSet<Profession> Professions { get; set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().Property(x => x.FullName).HasMaxLength(64);
            builder.Entity<Profession>().Property(x => x.Name).HasMaxLength(64);
            builder.Entity<Profession>().Property(x => x.Description).HasMaxLength(1024);
            base.OnModelCreating(builder);
        }


    }
}
