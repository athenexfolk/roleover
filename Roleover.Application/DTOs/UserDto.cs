namespace Roleover.Application.DTOs;

public class UserCreateDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class UserUpdateDto
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}