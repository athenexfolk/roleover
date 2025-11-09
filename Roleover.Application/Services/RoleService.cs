using Roleover.Application.DTOs;
using Roleover.Application.Exceptions;
using Roleover.Application.Interfaces;
using Roleover.Domain.Entities;

namespace Roleover.Application.Services;

public class RoleService(IRoleRepository roleRepository)
{
    private readonly IRoleRepository _roleRepository = roleRepository;

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
}
