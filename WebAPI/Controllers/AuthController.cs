using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;
[ApiController]
[Route("auth")]
public class AuthController:ControllerBase
{
    private readonly IUserRepository _userRepository;
    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public ActionResult<UserDto> Login([FromBody] LoginRequest request)
    {
        User? user = _userRepository.GetMany()
            .FirstOrDefault(u => u.username == request.UserName);
        if (user is null)
        {
            return Unauthorized("Invalid username or password");
        }

        if (user.password != request.Password)
        {
            return Unauthorized("Invalid username or password");
        }

        UserDto userDto = new()
        {
            Id = user.id,
            UserName = user.username
        };
        return Ok(userDto);
    }
}