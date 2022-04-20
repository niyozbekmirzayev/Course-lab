using Courselab.Service.Auth;
using Courselab.Service.Interfaces;
using Courselab.Service.Services;
using EduCenterWebAPI.Data.IRepositories;
using EduCenterWebAPI.Data.Respositories;
using Microsoft.Extensions.DependencyInjection;

namespace Courselab.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomService(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
