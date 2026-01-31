using FEMEE.Domain.Entities.Principal;

namespace FEMEE.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> GenerateTokenAsync(User user);
        Task<bool> ValidateTokenAsync(string token);
        Task<User?> GetUserFromTokenAsync(string token);
        Task<int> GetUserIdFromTokenAsync(string token);
    }
}
