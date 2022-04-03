using Courselab.Data.DbContexts;
using Courselab.Data.IRepositories;
using Courselab.Data.Respositories;
using Courselab.Domain.Entities.Students;

namespace Courselab.Data.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(CourselabDbContext context) : base(context)
        {
        }
    }
}
