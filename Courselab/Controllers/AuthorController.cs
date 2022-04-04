using Courselab.Domain.Commons;
using Courselab.Domain.Configurations;
using Courselab.Domain.Entities.Authors;
using Courselab.Service.DTOs.Authors;
using Courselab.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courselab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private IAuthorService authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Author>>> Create([FromForm] AuthorForCreationDto authorCreationalDTO)
        {
            var result = await authorService.CreateAsync(authorCreationalDTO);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public ActionResult<BaseResponse<IEnumerable<Author>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = authorService.GetAll(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Author>>> GetById(Guid id)
        {
            var result = await authorService.GetByIdAsync(id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete]
        public async Task<ActionResult<BaseResponse<Author>>> Delete(Guid id)
        {
            var result = await authorService.DeleteAsync(id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut]
        public async Task<ActionResult<BaseResponse<Author>>> Update([FromForm] AuthorForUpdateDto authorForUpdate)
        {
            var result = await authorService.UpdateAsync(authorForUpdate);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

    }
}
