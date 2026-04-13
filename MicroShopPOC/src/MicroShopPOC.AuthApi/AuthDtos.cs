namespace MicroShopPOC.AuthApi;

public record LoginRequest(string Username, string Password);
public record RegisterRequest(string Username, string Password);
public record TokenResponse(string Token);
