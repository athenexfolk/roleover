namespace Roleover.Application.DTOs;

public class PermissionCreateDto
{
    public required string Resource { get; set; }
    public required string Action { get; set; }
    public string? Description { get; set; }
}

public class PermissionUpdateDto
{
    public string? Resource { get; set; }
    public string? Action { get; set; }
    public string? Description { get; set; }
}