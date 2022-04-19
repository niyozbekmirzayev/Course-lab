using Courselab.Domain.Commons;
using Courselab.Domain.Configurations;
using Courselab.Domain.Entities.Courses;
using Courselab.Service.DTOs.Courses;
using Courselab.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courselab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private ICourseService courseService;

        public CoursesController(ICourseService courseService) => this.courseService = courseService;

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Course>>> Create([FromForm] CourseForCreationDto courseCreationalDto)
        {
            var result = await courseService.CreateAsync(courseCreationalDto);

            //  Identification of error 
            if (result.Error is not null)
            {
                if (result.Error.Code == 404) return NotFound(result);
                else if (result.Error.Code == 400) return BadRequest(result);
                else if (result.Error.Code == 409) return Conflict(result);
            }

            return Ok(result);
        }

        [HttpGet]
        public ActionResult<BaseResponse<IEnumerable<Course>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = courseService.GetAll(@params);

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
        public async Task<ActionResult<BaseResponse<Course>>> GetById(Guid id)
        {
            var result = await courseService.GetByIdAsync(id);

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
        public async Task<ActionResult<BaseResponse<Course>>> Delete(Guid id)
        {
            var result = await courseService.DeleteAsync(id);

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
        public async Task<ActionResult<BaseResponse<Course>>> Update([FromForm] CourseForUpdateDto courseForUpdate)
        {
            var result = await courseService.UpdateAsync(courseForUpdate);

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
