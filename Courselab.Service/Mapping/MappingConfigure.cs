using AutoMapper;
using Courselab.Domain.Entities.Authors;
using Courselab.Domain.Entities.Courses;
using Courselab.Domain.Entities.Students;
using Courselab.Service.DTOs.Authors;
using Courselab.Service.DTOs.Courses;
using Courselab.Service.DTOs.Students;

namespace Courselab.Service.Mapping
{
    public class MappingConfigure : Profile
    {
        public MappingConfigure()
        {
            CreateMap<Student, StudentForCreationDto>().ReverseMap();
            CreateMap<Author, AuthorForCreationDto>().ReverseMap();
            CreateMap<Course, CourseForCreationDto>().ReverseMap();
        }
    }
}
