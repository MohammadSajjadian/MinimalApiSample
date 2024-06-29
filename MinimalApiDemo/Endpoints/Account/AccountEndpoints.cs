using ApiDemo.Application.Dto;
using ApiDemo.Application.Enum;
using ApiDemo.Application.Mapper.User;
using ApiDemo.Application.Repositories.Account;

namespace MinimalApiDemo.Endpoints.Account;

public static class AccountEndpoints
{
    public static async Task<IResult> Register(RegisterDto dto, IUserRepository userRepository)
    {
        UserStatus status = await userRepository.Register(dto);

        if (status is UserStatus.UserExist)
            return Results.BadRequest("User already exist.");

        if (status is UserStatus.RegisterationFailed)
            return Results.BadRequest("Registration failed.");

        return Results.Ok(UserStatus.RegistrationSucceed.ToString());
    }

    public static async Task<IResult> Login(LoginDto dto, IUserRepository userRepository)
    {
        var status = await userRepository.Login(dto);

        if (status is UserStatus.UserNotFound)
            return Results.NotFound("Cant find user.");

        if (status is UserStatus.LoginFailed)
            return Results.BadRequest("Failed to log you in.");

        return Results.Ok();
    }

    public static async Task<IResult> GetUser(string userName, IUserRepository userRepository, IUserMapper mapper)
    {
        UserDto? user = await userRepository.GetUser(userName);

        if (user is null)
            return Results.NotFound("Cant find user.");

        return Results.Ok(mapper.Map(user));
    }

    public static async Task<IResult> GetCurrentUser(IUserRepository userRepository, IUserMapper mapper)
    {
        UserDto? user = await userRepository.GetCurrentUser();

        if (user is null)
            return Results.NotFound("Cant find user.");

        return Results.Ok(mapper.Map(user));
    }

    public static async Task<IResult> LogOut(IUserRepository userRepository)
    {
        await userRepository.LogOut();
        return Results.NoContent();
    }
}
