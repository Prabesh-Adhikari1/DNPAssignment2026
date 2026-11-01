using ApiContracts;

namespace BlazorApp.Services;

public interface IUserService
{
    Task<UserDto> AddUserAsync(CreateUserDto user);
    Task<List<UserDto>> GetUsersAsync();
}