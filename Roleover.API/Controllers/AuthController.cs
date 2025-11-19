using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roleover.Application.DTOs;
using Roleover.Application.Interfaces;
using Roleover.Application.Exceptions;
using System.Security.Claims;

namespace Roleover.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthProvider authProvider, IUserRepository userRepository, IRoleRepository roleRepository) : ControllerBase
{
    private readonly IAuthProvider _authProvider = authProvider;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthRequest request)
    {
        var token = await _authProvider.AuthenticateAsync(request.Username, request.Password) ?? throw new UnauthorizedException("Invalid credentials");
        return Ok(new AuthResponse(token));
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized();
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return Unauthorized();
        var roles = await _roleRepository.GetRolesByUserIdAsync(user.Id);
        foreach (var role in roles) user.AddRole(role);

        return Ok(user);
    }

    [Authorize]
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok(User.Identity?.Name ?? "Anonymous");
    }

    [Authorize("AdminOnly")]
    [HttpGet("admin-ping")]
    public IActionResult AdminPing()
    {
        return Ok("Hello, Admin!");
    }
}