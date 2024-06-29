using ApiDemo.Application.Dto;
using ApiDemo.Application.Enum;
using ApiDemo.Application.Mapper.User;
using ApiDemo.Application.Repositories.Account;
using ApiDemo.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ApiDemo.Infrastructure.Sql.Services.Account;

public class UserRepository(
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    IHttpContextAccessor _httpContext,
    IUserMapper _mapper) : IUserRepository
{
    public async Task<UserStatus> Register(RegisterDto dto)
    {
        UserDto? userDto = await GetUser(dto.UserName);
        if (userDto is not null)
            return UserStatus.UserExist;

        ApplicationUser? user = new()
        {
            UserName = dto.UserName,
            Email = dto.UserName,
            EmailConfirmed = true
        };

        var status = await _userManager.CreateAsync(user, dto.Password!);
        if (!status.Succeeded)
            return UserStatus.RegisterationFailed;

        return UserStatus.RegistrationSucceed;
    }

    public async Task<UserStatus> Login(LoginDto loginDto)
    {
        UserDto? userDto = await GetUser(loginDto.UserName);

        if (userDto is null)
            return UserStatus.UserNotFound;

        var status = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, loginDto.RememberMe, true);
        if (!status.Succeeded)
            return UserStatus.LoginFailed;

        return UserStatus.LoginSucceed;
    }

    public async Task<UserDto?> GetUser(string userName)
    {
        ApplicationUser? user = await _userManager.FindByNameAsync(userName);
        return _mapper.Map(user);
    }

    public async Task<UserDto?> GetCurrentUser()
    {
        ApplicationUser? user = await _userManager.FindByNameAsync(_httpContext.HttpContext.User.Identity!.Name!);
        return _mapper.Map(user);
    }

    public async Task LogOut() =>
        await _signInManager.SignOutAsync();
}
