using Courselab.Data.DbContexts;
using Courselab.Data.IRepositories;
using Courselab.Data.Respositories;
using Courselab.Domain.Entities.Authors;

namespace Courselab.Data.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(CourselabDbContext context) : base(context)
        {
        }
    }
}
