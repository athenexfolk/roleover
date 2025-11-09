using Roleover.Application.DTOs;
using Roleover.Application.Exceptions;
using Roleover.Application.Interfaces;
using Roleover.Domain.Entities;

namespace Roleover.Application.Services;

public class PermissionService(IPermissionRepository permissionRepository)
{
    private readonly IPermissionRepository _permissionRepository = permissionRepository;

    public async Task<Permission> CreatePermissionAsync(PermissionCreateDto dto)
    {
        var permission = new Permission(dto.Resource, dto.Action, dto.Description);
        await _permissionRepository.AddAsync(permission);
        return permission;
    }

    public async Task<Permission> UpdatePermissionAsync(string id, PermissionUpdateDto dto)
    {
        var permission = await _permissionRepository.GetByIdAsync(id) ?? throw new NotFoundException("Permission not found.");
        if (!string.IsNullOrEmpty(dto.Resource))
        {
            permission.UpdateResource(dto.Resource);
        }
        if (!string.IsNullOrEmpty(dto.Action))
        {
            permission.UpdateAction(dto.Action);
        }
        if (!string.IsNullOrEmpty(dto.Description))
        {
            permission.UpdateDescription(dto.Description);
        }
        await _permissionRepository.UpdateAsync(permission);
        return permission;
    }

    public async Task<bool> DeletePermissionAsync(string id)
    {
        var permission = await _permissionRepository.GetByIdAsync(id) ?? throw new NotFoundException("Permission not found.");
        await _permissionRepository.DeleteAsync(permission);
        return true;
    }

    public async Task<Permission> GetPermissionByIdAsync(string id)
    {
        return await _permissionRepository.GetByIdAsync(id) ?? throw new NotFoundException("Permission not found.");
    }

    public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
    {
        return await _permissionRepository.GetAllAsync();
    }
}
