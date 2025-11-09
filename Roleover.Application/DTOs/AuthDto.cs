namespace Roleover.Application.DTOs;

public record AuthRequest(string Username, string Password);
public record AuthResponse(string Token);