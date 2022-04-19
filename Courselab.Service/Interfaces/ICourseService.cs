using Courselab.Domain.Commons;
using Courselab.Domain.Configurations;
using Courselab.Domain.Entities.Courses;
using Courselab.Service.DTOs.Courses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courselab.Service.Interfaces
{
    public interface ICourseService
    {
        Task<BaseResponse<Course>> CreateAsync(CourseForCreationDto courseCreationalDTO);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
        Task<BaseResponse<Course>> GetByIdAsync(Guid id);
        BaseResponse<IEnumerable<Course>> GetAll(PaginationParams @params);
        Task<BaseResponse<Course>> UpdateAsync(CourseForUpdateDto courseToUpdate);

        void RefitImage(Course course);
        void RefitVideo(Course course);
    }
}
