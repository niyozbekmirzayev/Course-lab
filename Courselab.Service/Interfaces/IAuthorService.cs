using Courselab.Domain.Commons;
using Courselab.Domain.Configurations;
using Courselab.Service.DTOs.Authors;
using Courselab.Service.DTOs.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Courselab.Service.Interfaces
{
    public interface IAuthorService
    {
        Task<BaseResponse<Author>> CreateAsync(AuthorForCreationDto authorCreationalDTO);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
        Task<BaseResponse<Author>> GetByIdAsync(Guid id);
        BaseResponse<IEnumerable<Author>> GetAll(PaginationParams @params);
        Task<BaseResponse<Author>> UpdateAsync(AuthorForUpdateDto authorToUpdate);
        Task<BaseResponse<Author>> SetImageAsync(SetImageDto setImageDto);
        Task<string> SaveFileAsync(Stream file, string fileName);
        void RefitImage(Author author);
    }
}
