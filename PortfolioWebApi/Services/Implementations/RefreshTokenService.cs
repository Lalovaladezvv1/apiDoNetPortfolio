using StackExchange.Redis;
using System.Text.Json;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IDatabase _redis;

    public RefreshTokenService(IConnectionMultiplexer redis)
    {
        _redis = redis.GetDatabase();
    }

    public async Task SaveAsync(string token, int userId)
    {
        var data = JsonSerializer.Serialize(new
        {
            userId,
            createdAt = DateTime.UtcNow
        });

        await _redis.StringSetAsync(
            $"refresh:{token}",
            data,
            TimeSpan.FromDays(7)
        );
    }

    public async Task<int?> ValidateAsync(string token)
    {
        var value = await _redis.StringGetAsync($"refresh:{token}");
        if (value.IsNullOrEmpty) return null;

        var json = JsonSerializer.Deserialize<JsonElement>(value);
        return json.GetProperty("userId").GetInt32();
    }

    public async Task RevokeAsync(string token)
    {
        await _redis.KeyDeleteAsync($"refresh:{token}");
    }
}
