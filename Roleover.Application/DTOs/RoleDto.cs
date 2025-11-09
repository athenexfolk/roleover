namespace Roleover.Application.DTOs;

public class RoleCreateDto
{
    public required string Name { get; set; }
}

public class RoleUpdateDto
{
    public string Name { get; set; } = string.Empty;
}
