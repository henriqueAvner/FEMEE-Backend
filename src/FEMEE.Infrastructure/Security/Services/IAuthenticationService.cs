using FEMEE.Domain.Entities.Principal;
using System;
using System.Collections.Generic;
using System.Text;

namespace FEMEE.Infrastructure.Security.Services
{
    public interface IAuthenticationService
    {
        Task<string> GenerateToken(User user);

        Task<bool> ValidateToken(string token);

        Task<User?> GetUserFromToken(string token);

        Task<int> GetUserIdFromTokenAsync(string token);
    }
}
