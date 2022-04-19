using Courselab.Data.DbContexts;
using Courselab.Data.IRepositories;
using Courselab.Data.Respositories;
using Courselab.Domain.Entities.Users;

namespace Courselab.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(CourselabDbContext context) : base(context)
        {
        }
    }
}
