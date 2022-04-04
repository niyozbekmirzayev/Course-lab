using Courselab.Domain.Entities.Authors;
using Courselab.Domain.Entities.Courses;
using Courselab.Domain.Entities.Registraions;
using Courselab.Domain.Entities.Students;
using Microsoft.EntityFrameworkCore;

namespace Courselab.Data.DbContexts
{
    public class CourselabDbContext : DbContext
    {
        public CourselabDbContext(DbContextOptions<CourselabDbContext> options)
           : base(options)
        {

        }

        DbSet<Author> Authors { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().Property(p => p.Price).HasColumnType("decimal(18,4)");
        }
    }
}
