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

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Course>>> Create([FromForm] CourseForCreationDto courseCreationalDto)
        {
            var result = await courseService.CreateAsync(courseCreationalDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public ActionResult<BaseResponse<IEnumerable<Course>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = courseService.GetAll(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> GetById(Guid id)
        {
            var result = await courseService.GetByIdAsync(id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete]
        public async Task<ActionResult<BaseResponse<Course>>> Delete(Guid id)
        {
            var result = await courseService.DeleteAsync(id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut]
        public async Task<ActionResult<BaseResponse<Course>>> Update([FromForm] CourseForUpdateDto courseForUpdate)
        {
            var result = await courseService.UpdateAsync(courseForUpdate);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

    }
}
