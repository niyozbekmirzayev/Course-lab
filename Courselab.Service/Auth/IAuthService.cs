using Courselab.Domain.Entities.Users;

namespace Courselab.Service.Auth
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
