using Courselab.Data.DbContexts;
using Courselab.Data.IRepositories;
using Courselab.Data.Respositories;
using Courselab.Domain.Entities.Courses;

namespace Courselab.Data.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(CourselabDbContext context) : base(context)
        {
        }
    }
}
