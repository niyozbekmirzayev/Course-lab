using Courselab.Data.DbContexts;
using Courselab.Data.IRepositories;
using Courselab.Data.Repositories;
using EduCenterWebAPI.Data.IRepositories;
using System;
using System.Threading.Tasks;

namespace EduCenterWebAPI.Data.Respositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CourselabDbContext context;

        public ICourseRepository Courses { get; private set; }

        public IStudentRepository Students { get; private set; }

        public IAuthorRepository Authors { get; private set; }

        public UnitOfWork(CourselabDbContext context)
        {
            this.context = context;
            Courses = new CourseRepository(context);
            Students = new StudentRepository(context);
            Authors = new AuthorRepository(context);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
