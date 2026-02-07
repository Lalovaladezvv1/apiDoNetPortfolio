public interface IRefreshTokenService
{
    Task SaveAsync(string token, int userId);
    Task<int?> ValidateAsync(string token);
    Task RevokeAsync(string token);
}