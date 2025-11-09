using Microsoft.AspNetCore.Mvc;
using Roleover.Application.DTOs;
using Roleover.Application.Services;
using Roleover.Application.Exceptions;

namespace Roleover.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController(RoleService roleService) : ControllerBase
{
    private readonly RoleService _roleService = roleService;

    [HttpPost]
    public async Task<IActionResult> CreateRole(RoleCreateDto dto)
    {
        var role = await _roleService.CreateRoleAsync(dto);
        return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, role);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _roleService.GetAllRolesAsync();
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(string id)
    {
        var role = await _roleService.GetRoleByIdAsync(id) ?? throw new NotFoundException($"Role with id '{id}' not found");
        return Ok(role);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(string id, RoleUpdateDto dto)
    {
        var result = await _roleService.UpdateRoleAsync(id, dto) ?? throw new NotFoundException($"Role with id '{id}' not found");
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var result = await _roleService.DeleteRoleAsync(id);
        if (!result)
        {
            throw new NotFoundException($"Role with id '{id}' not found");
        }
        return NoContent();
    }
}
