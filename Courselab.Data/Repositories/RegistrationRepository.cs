using Courselab.Data.DbContexts;
using Courselab.Data.IRepositories;
using Courselab.Data.Respositories;
using Courselab.Domain.Entities.Registraions;

namespace Courselab.Data.Repositories
{
    public class RegistrationRepository : GenericRepository<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(CourselabDbContext context) : base(context)
        {
        }
    }
}
