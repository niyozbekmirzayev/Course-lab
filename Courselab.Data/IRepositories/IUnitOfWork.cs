using Courselab.Data.IRepositories;
using System;
using System.Threading.Tasks;

namespace EduCenterWebAPI.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ICourseRepository Courses { get; }
        IRegistrationRepository Registrations { get; }
        Task SaveChangesAsync();
    }
}
