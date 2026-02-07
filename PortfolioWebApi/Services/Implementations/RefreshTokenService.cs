using System.Net.Http.Headers;
using System.Text.Json;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly HttpClient _http;
    private readonly string _url;

    public RefreshTokenService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _url = config["REDIS_REST_URL"]!;

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                config["REDIS_REST_TOKEN"]!
            );
    }

    public async Task SaveAsync(string refreshToken, int userId)
    {
        var key = Uri.EscapeDataString(refreshToken);

        var response = await _http.PostAsync(
            $"{_url}/set/{key}/{userId}?ex=604800",
            null
        );

        response.EnsureSuccessStatusCode();
    }

    public async Task<int?> ValidateAsync(string refreshToken)
    {
        var key = Uri.EscapeDataString(refreshToken);

        var response = await _http.GetAsync($"{_url}/get/{key}");
        if (!response.IsSuccessStatusCode)
            return null;

        var jsonString = await response.Content.ReadAsStringAsync();

        var json = JsonSerializer.Deserialize<JsonElement>(jsonString);

        if (!json.TryGetProperty("result", out var result))
            return null;

        if (result.ValueKind == JsonValueKind.Null)
            return null;

        return int.Parse(result.GetString()!);
    }

    public async Task RevokeAsync(string refreshToken)
    {
        var key = Uri.EscapeDataString(refreshToken);
        await _http.PostAsync($"{_url}/del/{key}", null);
    }
}
