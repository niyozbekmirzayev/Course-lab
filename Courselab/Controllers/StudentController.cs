using Courselab.Domain.Commons;
using Courselab.Domain.Entities.Students;
using Courselab.Service.DTOs.Students;
using Courselab.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Courselab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class studentController : Controller
    {
        private IStudentService studentService;

        public studentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Student>>> Create([FromForm] StudentForCreationDto studentCreationalDTO)
        {
            var result = await studentService.CreateAsync(studentCreationalDTO);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

    }
}
