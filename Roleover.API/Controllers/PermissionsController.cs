using Microsoft.AspNetCore.Mvc;
using Roleover.Application.DTOs;
using Roleover.Application.Services;
using Roleover.Application.Exceptions;

namespace Roleover.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController(PermissionService permissionService) : ControllerBase
{
    private readonly PermissionService _permissionService = permissionService;

    [HttpPost]
    public async Task<IActionResult> CreatePermission(PermissionCreateDto dto)
    {
        var permission = await _permissionService.CreatePermissionAsync(dto);
        return CreatedAtAction(nameof(GetPermissionById), new { id = permission.Id }, permission);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPermissions()
    {
        var permissions = await _permissionService.GetAllPermissionsAsync();
        return Ok(permissions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPermissionById(string id)
    {
        var permission = await _permissionService.GetPermissionByIdAsync(id) ?? throw new NotFoundException($"Permission with id '{id}' not found");
        return Ok(permission);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePermission(string id, PermissionUpdateDto dto)
    {
        var result = await _permissionService.UpdatePermissionAsync(id, dto) ?? throw new NotFoundException($"Permission with id '{id}' not found");
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePermission(string id)
    {
        var result = await _permissionService.DeletePermissionAsync(id);
        if (!result)
        {
            throw new NotFoundException($"Permission with id '{id}' not found");
        }
        return NoContent();
    }
}
