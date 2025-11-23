using Microsoft.AspNetCore.Mvc;
using Roleover.Application.DTOs;
using Roleover.Application.Services;
using Roleover.Application.Exceptions;

namespace Roleover.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(UserService userService) : ControllerBase
{
    private readonly UserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserCreateDto dto)
    {
        var user = await _userService.CreateUserAsync(dto);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetUsersPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _userService.GetUsersPagedAsync(page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userService.GetUserByIdAsync(id) ?? throw new NotFoundException($"User with id '{id}' not found");
        return Ok(user);
    }

    [HttpGet("by-username/{username}")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        var user = await _userService.GetUserByUsernameAsync(username) ?? throw new NotFoundException($"User with username '{username}' not found");
        return Ok(user);
    }

    [HttpGet("by-email/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email) ?? throw new NotFoundException($"User with email '{email}' not found");
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, UserUpdateDto dto)
    {
        var result = await _userService.UpdateUserAsync(id, dto) ?? throw new NotFoundException($"User with id '{id}' not found");
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var result = await _userService.DeleteUserAsync(id);
        if (!result)
        {
            throw new NotFoundException($"User with id '{id}' not found");
        }
        return NoContent();
    }

    [HttpPost("{id}/roles")]
    public async Task<IActionResult> AddRoleToUser(string id, AddRoleDto dto)
    {
        if (id != dto.UserId)
            return BadRequest("UserId in route and body must match.");
        await _userService.AddToRoleAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}/roles")]
    public async Task<IActionResult> RemoveRoleFromUser(string id, RemoveRoleDto dto)
    {
        if (id != dto.UserId)
            return BadRequest("UserId in route and body must match.");
        await _userService.RemoveFromRoleAsync(dto);
        return NoContent();
    }
}
