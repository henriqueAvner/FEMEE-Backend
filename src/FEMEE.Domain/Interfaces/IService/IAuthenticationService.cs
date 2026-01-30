using FEMEE.Domain.Entities.Principal;

namespace FEMEE.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> GenerateToken(User user);
        Task<bool> ValidateToken(string token);
        Task<User?> GetUserFromToken(string token);
        Task<int> GetUserIdFromTokenAsync(string token);
    }
}
