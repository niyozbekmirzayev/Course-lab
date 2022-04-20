using Courselab.Domain.Enums;
using Courselab.Service.Auth;
using Courselab.Service.DTOs.Users;
using Courselab.Service.Extensions;
using EduCenterWebAPI.Data.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Courselab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private readonly IUnitOfWork unitOfWork;

        public AuthController(IAuthService authService, IUnitOfWork unitOfWork)
        {
            this.authService = authService;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("login")]
        public async Task<IActionResult> CreateToken(UserLoginDto loginParams)
        {
            var user = await unitOfWork.Users.GetAsync(user => user.Username == loginParams.Username &&
                                                       user.Password == loginParams.Password.EncodeInSha256() &&
                                                       user.Status != ObjectStatus.Deleted);

            if (user is null) return NotFound("Login or password did not match");

            string token = authService.GenerateToken(user);

            return Ok(token);
        }
    }
}
