using ApiDemo.Application.Dto;
using ApiDemo.Application.Enum;

namespace ApiDemo.Application.Repositories.Account;

public interface IUserRepository
{
    Task<UserStatus> Register(RegisterDto dto);
    Task<UserStatus> Login(LoginDto dto);
    Task<UserDto?> GetUser(string userName);
    Task<UserDto?> GetCurrentUser();
    Task LogOut();
}
