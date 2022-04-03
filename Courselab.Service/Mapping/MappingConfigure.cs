﻿using AutoMapper;
using Courselab.Domain.Entities.Students;
using Courselab.Service.DTOs.Students;

namespace Courselab.Service.Mapping
{
    public class MappingConfigure : Profile
    {
        public MappingConfigure()
        {
            CreateMap<Student, StudentForCreationDto>().ReverseMap();
        }
    }
}