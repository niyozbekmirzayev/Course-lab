using AutoMapper;
using Courselab.Domain.Entities.Courses;
using Courselab.Domain.Entities.Users;
using Courselab.Service.DTOs.Courses;
using Courselab.Service.DTOs.Users;

namespace Courselab.Service.Mapping
{
    public class MappingConfigure : Profile
    {
        public MappingConfigure()
        {
            CreateMap<User, UserForCreationDto>().ReverseMap();
            CreateMap<Course, CourseForCreationDto>().ReverseMap();
        }
    }
}
