using Courselab.Data.IRepositories;
using System;
using System.Threading.Tasks;

namespace EduCenterWebAPI.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        ICourseRepository Courses { get; }
        IAuthorRepository Authors { get; }
        IRegistrationRepository Registrations { get; }
        Task SaveChangesAsync();
    }
}
