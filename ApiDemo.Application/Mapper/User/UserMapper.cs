using ApiDemo.Application.Dto;
using ApiDemo.Domain.Entities;

namespace ApiDemo.Application.Mapper.User;

public class UserMapper : IUserMapper
{
    public ApplicationUser? Map(UserDto? userDto)
    {
        return userDto is not null ? new ApplicationUser
        {
            UserName = userDto.UserName,
            Email = userDto.Email,
        }
        : null;
    }

    public UserDto? Map(ApplicationUser? user)
    {
        return user is not null ? new UserDto(user.UserName!, user.Email!) : null;
    }
}
