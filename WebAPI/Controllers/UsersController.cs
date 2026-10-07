using ApiContracts;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UsersController: ControllerBase
{
    private readonly IUserRepository _userRepository;
    
    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public ActionResult<IQueryable<User>> GetMany([FromQuery]string? userName)
    {
        IQueryable<User> users = _userRepository.GetMany();

        if (!string.IsNullOrEmpty(userName))
        {
            users = users.Where(user => user.username.
                Contains(userName));
        }
        IEnumerable<UserDto> dtos = users.Select(user => new UserDto
        {
            Id = user.id,
            UserName = user.username
        });

        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetSingle(int id)
    {
        User? user = await _userRepository.GetSingleAsync(id);
        if (user is null)
        {
            return NotFound();
        }
        UserDto dto = new UserDto
        {
            Id = user.id,
            UserName = user.username
        };


        return Ok(dto);
    }
    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request)
    {
        bool usernameexists = _userRepository.GetMany().Any(user =>
            user.username == request.UserName);
        if (usernameexists)
        {
            return BadRequest("Username already exists");
        }
        User user = new User
        {
            username = request.UserName,
            password = request.Password,
        };

        User createdUser = await _userRepository.AddAsync(user);
        UserDto dto = new UserDto
        {
            Id = createdUser.id,
            UserName = createdUser.username
        };

        return Created($"/Users/{dto.Id}",dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserDto
        request)
    {
        User user = new User
        {
            id=id,
            username = request.UserName,
            password = request.Password
        };

        await _userRepository.UpdateAsync(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletUser
        (int id)
    {
        await _userRepository.DeleteAsync(id);
        return NoContent();
    }
}