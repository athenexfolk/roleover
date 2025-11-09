using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roleover.Application.DTOs;
using Roleover.Application.Interfaces;
using Roleover.Application.Exceptions;

namespace Roleover.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthProvider authProvider) : ControllerBase
{
    private readonly IAuthProvider _authProvider = authProvider;

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthRequest request)
    {
        var token = await _authProvider.AuthenticateAsync(request.Username, request.Password) ?? throw new UnauthorizedException("Invalid credentials");
        return Ok(new AuthResponse(token));
    }

    [Authorize]
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok(User.Identity?.Name ?? "Anonymous");
    }
}