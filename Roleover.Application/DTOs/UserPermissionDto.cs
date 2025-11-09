namespace Roleover.Application.DTOs;

public record GrantUserPermissionDto(string UserId, string PermissionId);
public record RevokeUserPermissionDto(string UserId, string PermissionId);