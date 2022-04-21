using Courselab.Domain.Entities.Users;

namespace Courselab.Service.Services
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
