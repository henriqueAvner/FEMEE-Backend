using FEMEE.Application.DTOs.User;

namespace FEMEE.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> GetUserByIdAsync(int id);
        Task<UserResponseDto> CreateUserAsync(CreateUserDto dto);
    }
}
