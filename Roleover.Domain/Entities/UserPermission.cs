namespace Roleover.Domain.Entities;

public class UserPermission(string userId, string permissionId)
{
    public string Id { get; } = Guid.NewGuid().ToString();

    public string UserId { get; set; } = userId;
    public string PermissionId { get; set; } = permissionId;
}
