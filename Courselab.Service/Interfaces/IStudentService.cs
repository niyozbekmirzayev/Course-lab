using Courselab.Domain.Commons;
using Courselab.Domain.Configurations;
using Courselab.Domain.Entities.Students;
using Courselab.Service.DTOs.Students;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Courselab.Service.Interfaces
{
    public interface IStudentService
    {
        Task<BaseResponse<Student>> CreateAsync(StudentForCreationDto studentCreationalDTO);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
        Task<BaseResponse<Student>> GetByIdAsync(Guid id);
        BaseResponse<IEnumerable<Student>> GetAll(PaginationParams @params);
        Task<BaseResponse<Student>> UpdateAsync(StudentForUpdateDto studentToUpdate);
        Task<string> SaveFileAsync(Stream file, string fileName);
    }
}
