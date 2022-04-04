using Courselab.Domain.Commons;
using Courselab.Domain.Configurations;
using Courselab.Domain.Entities.Students;
using Courselab.Service.DTOs.Students;
using Courselab.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courselab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Student>>> Create([FromForm] StudentForCreationDto studentCreationalDTO)
        {
            var result = await studentService.CreateAsync(studentCreationalDTO);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public ActionResult<BaseResponse<IEnumerable<Student>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = studentService.GetAll(@params);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Student>>> GetById(Guid id)
        {
            var result = await studentService.GetByIdAsync(id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete]
        public async Task<ActionResult<BaseResponse<Student>>> Delete(Guid id)
        {
            var result = await studentService.DeleteAsync(id);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut]
        public async Task<ActionResult<BaseResponse<Student>>> Update([FromForm] StudentForUpdateDto studentForUpdate)
        {
            var result = await studentService.UpdateAsync(studentForUpdate);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<BaseResponse<Student>>> BuyCourse(Guid studentId, Guid courseId)
        {
            var result = await studentService.BuyCourseAsync(studentId, courseId);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

    }
}
