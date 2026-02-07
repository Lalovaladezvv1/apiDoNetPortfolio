using System.Net.Http.Headers;

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
        var response = await _http.PostAsync(
            $"{_url}/set/{refreshToken}/{userId}?ex=604800",
            null
        );

        response.EnsureSuccessStatusCode();
    }

    public async Task<int?> ValidateAsync(string refreshToken)
    {
        var response = await _http.GetAsync($"{_url}/get/{refreshToken}");

        if (!response.IsSuccessStatusCode)
            return null;

        var value = await response.Content.ReadAsStringAsync();

        if (value == "null")
            return null;

        return int.Parse(value);
    }

    public async Task RevokeAsync(string refreshToken)
    {
        await _http.PostAsync($"{_url}/del/{refreshToken}", null);
    }
}
