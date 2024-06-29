using ApiDemo.Application.Dto;
using ApiDemo.Domain.Entities;

namespace ApiDemo.Application.Mapper.User;

public interface IUserMapper
{
    ApplicationUser? Map(UserDto? applicationUserDto);
    UserDto? Map(ApplicationUser? applicationUser);
}
