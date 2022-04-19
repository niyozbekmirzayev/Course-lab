using Courselab.Domain.Commons;
using Courselab.Domain.Configurations;
using Courselab.Domain.Entities.Users;
using Courselab.Service.DTOs.Commons;
using Courselab.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Courselab.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<User>> CreateAsync(UserForCreationDto userCreationalDTO);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
        Task<BaseResponse<User>> GetByIdAsync(Guid id);
        BaseResponse<IEnumerable<User>> GetAll(PaginationParams @params);
        Task<BaseResponse<User>> UpdateAsync(UserForUpdateDto userToUpdate);
        Task<BaseResponse<User>> BuyCourseAsync(Guid userId, Guid courseId);
        Task<BaseResponse<User>> SetImageAsync(SetImageDto setImageDto);

        Task<string> SaveFileAsync(Stream file, string fileName);
        void RefitImage(User user);
    }
}
