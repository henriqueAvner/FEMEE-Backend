using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEMEE.Domain.Entities.Principal;

namespace FEMEE.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateAsync(string email, string senha);
        Task<string> GenerateToken(User user);
        Task<bool> ValidateToken(string token);
        Task<User> GetUserFromTokenAsync(string token);
        string HasPassword(string senha);
        bool VerifyPassword(string senha, string hash);
        Task LogoutAsync(string token);
    }
}