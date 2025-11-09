namespace Roleover.Domain.Entities;

public class RolePermission(string roleId, string permissionId)
{
    public string Id { get; } = Guid.NewGuid().ToString();

    public string RoleId { get; set; } = roleId;
    public string PermissionId { get; set; } = permissionId;
}
