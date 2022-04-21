using Courselab.Domain.Commons;
using Courselab.Domain.Configurations;
using Courselab.Domain.Entities.Users;
using Courselab.Service.DTOs.Commons;
using Courselab.Service.DTOs.Users;
using Courselab.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courselab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IUserService userService;
        public UsersController(IUserService userService) => this.userService = userService;

        [HttpPost]
        public async Task<ActionResult<BaseResponse<User>>> Create([FromForm] UserForCreationDto userCreationalDto)
        {
            var result = await userService.CreateAsync(userCreationalDto);

            // Identification of error 
            if (result.Error is not null)
            {
                if (result.Error.Code == 404) return NotFound(result);
                else if (result.Error.Code == 400) return BadRequest(result);
                else if (result.Error.Code == 409) return Conflict(result);
            }

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<BaseResponse<IEnumerable<User>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = userService.GetAll(@params);

            // Identification of error 
            if (result.Error is not null)
            {
                if (result.Error.Code == 404) return NotFound(result);
                else if (result.Error.Code == 400) return BadRequest(result);
                else if (result.Error.Code == 409) return Conflict(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<User>>> GetById(Guid id)
        {
            var result = await userService.GetByIdAsync(id);

            // Identification of error 
            if (result.Error is not null)
            {
                if (result.Error.Code == 404) return NotFound(result);
                else if (result.Error.Code == 400) return BadRequest(result);
                else if (result.Error.Code == 409) return Conflict(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<BaseResponse<User>>> Delete(Guid id)
        {
            var result = await userService.DeleteAsync(id);

            // Identification of error 
            if (result.Error is not null)
            {
                if (result.Error.Code == 404) return NotFound(result);
                else if (result.Error.Code == 400) return BadRequest(result);
                else if (result.Error.Code == 409) return Conflict(result);
            }

            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<BaseResponse<User>>> Update([FromForm] UserForUpdateDto userForUpdate)
        {
            var result = await userService.UpdateAsync(userForUpdate);

            // Identification of error 
            if (result.Error is not null)
            {
                if (result.Error.Code == 404) return NotFound(result);
                else if (result.Error.Code == 400) return BadRequest(result);
                else if (result.Error.Code == 409) return Conflict(result);
            }

            return Ok(result);
        }

        [HttpPost("[action]/{id}&{courseId}")]
        [Authorize]
        public async Task<ActionResult<BaseResponse<User>>> RegisterCourse(Guid id, Guid courseId)
        {
            var result = await userService.BuyCourseAsync(id, courseId);

            // Identification of error 
            if (result.Error is not null)
            {
                if (result.Error.Code == 404) return NotFound(result);
                else if (result.Error.Code == 400) return BadRequest(result);
                else if (result.Error.Code == 409) return Conflict(result);
            }

            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult<BaseResponse<User>>> SetImage([FromForm] SetImageDto setImageDto)
        {
            var result = await userService.SetImageAsync(setImageDto);

            // Identification of error 
            if (result.Error is not null)
            {
                if (result.Error.Code == 404) return NotFound(result);
                else if (result.Error.Code == 400) return BadRequest(result);
                else if (result.Error.Code == 409) return Conflict(result);
            }

            return Ok(result);
        }
    }
}
