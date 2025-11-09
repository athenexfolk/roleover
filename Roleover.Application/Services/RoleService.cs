using Roleover.Application.DTOs;
using Roleover.Application.Exceptions;
using Roleover.Application.Interfaces;
using Roleover.Domain.Entities;

namespace Roleover.Application.Services;

public class RoleService(IRoleRepository roleRepository, IPermissionRepository permissionRepository)
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IPermissionRepository _permissionRepository = permissionRepository;

    public async Task<Role> CreateRoleAsync(RoleCreateDto dto)
    {
        var role = new Role(dto.Name);
        await _roleRepository.AddAsync(role);
        return role;
    }

    public async Task<Role> UpdateRoleAsync(string id, RoleUpdateDto dto)
    {
        var role = await _roleRepository.GetByIdAsync(id) ?? throw new NotFoundException("Role not found.");
        if (!string.IsNullOrEmpty(dto.Name))
        {
            role.UpdateName(dto.Name);
        }
        await _roleRepository.UpdateAsync(role);
        return role;
    }

    public async Task<bool> DeleteRoleAsync(string id)
    {
        var role = await _roleRepository.GetByIdAsync(id) ?? throw new NotFoundException("Role not found.");
        await _roleRepository.DeleteAsync(role);
        return true;
    }

    public async Task<Role> GetRoleByIdAsync(string id)
    {
        return await _roleRepository.GetByIdAsync(id) ?? throw new NotFoundException("Role not found.");
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return await _roleRepository.GetAllAsync();
    }

    public async Task GrantPermissionAsync(GrantRolePermissionDto dto)
    {
        var role = await _roleRepository.GetByIdAsync(dto.RoleId) ?? throw new NotFoundException("Role not found.");
        var permission = await _permissionRepository.GetByIdAsync(dto.PermissionId) ?? throw new NotFoundException("Permission not found.");

        await _roleRepository.GrantPermissionAsync(role, permission);
    }

    public async Task RevokePermissionAsync(RevokeRolePermissionDto dto)
    {
        var role = await _roleRepository.GetByIdAsync(dto.RoleId) ?? throw new NotFoundException("Role not found.");
        var permission = await _permissionRepository.GetByIdAsync(dto.PermissionId) ?? throw new NotFoundException("Permission not found.");

        await _roleRepository.RevokePermissionAsync(role, permission);
    }
}