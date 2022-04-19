using Courselab.Domain.Commons;
using Courselab.Domain.Configurations;
using Courselab.Domain.Entities.Users;
using Courselab.Service.DTOs.Commons;
using Courselab.Service.DTOs.Users;
using Courselab.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Courselab.Service.Services
{
    public class UserService : IUserService
    {
        public Task<BaseResponse<User>> BuyCourseAsync(Guid userId, Guid courseId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<User>> CreateAsync(UserForCreationDto userCreationalDTO)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<IEnumerable<User>> GetAll(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<User>> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void RefitImage(User user)
        {
            throw new NotImplementedException();
        }

        public Task<string> SaveFileAsync(Stream file, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<User>> SetImageAsync(SetImageDto setImageDto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<User>> UpdateAsync(UserForUpdateDto userToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
