using Courselab.Data.DbContexts;
using Courselab.Data.IRepositories;
using Courselab.Data.Respositories;

namespace Courselab.Data.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IUserRepository
    {
        public StudentRepository(CourselabDbContext context) : base(context)
        {
        }
    }
}
