using Courselab.Domain.Entities.Courses;
using Courselab.Domain.Entities.Registraions;
using Courselab.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Courselab.Data.DbContexts
{
    public class CourselabDbContext : DbContext
    {
        public CourselabDbContext(DbContextOptions<CourselabDbContext> options)
           : base(options)
        {

        }

        DbSet<User> Users { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Registration> Registrations { get; set; }
    }
}
