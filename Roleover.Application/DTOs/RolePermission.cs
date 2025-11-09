namespace Roleover.Application.DTOs;

public record GrantRolePermissionDto(string RoleId, string PermissionId);
public record RevokeRolePermissionDto(string RoleId, string PermissionId);
