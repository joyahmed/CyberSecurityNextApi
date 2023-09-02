using CyberSecurityNextApi.Dtos.User;
using System;

namespace CyberSecurityNextApi.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();

        Task<ServiceResponse<int>> Register(User user, string password);

        Task<ServiceResponse<string>> Login(string username, string email, string password);

        Task<ServiceResponse<GetUserDto>> UpdateUser(int? userId, UpdateUserDto updatedUser);

        Task<ServiceResponse<GetUserDto>> GetUserById(int id);

        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);

        Task<bool> UserExists(string username);
    }
}
