namespace MicroShopPOC.AuthApi;

public interface IAuthService
{
    Task<TokenResponse?> LoginAsync(LoginRequest request);
    Task<TokenResponse?> RegisterAsync(RegisterRequest request);
}
