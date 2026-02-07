using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebApi.Models;
using System.Security.Cryptography;
using System.Text;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly FreeSqlDb2016559Context _context;
    private readonly IConfiguration _config;
    private readonly IRefreshTokenService _refreshTokenService;

    public AuthController(
        FreeSqlDb2016559Context context,
        IConfiguration config,
        IRefreshTokenService refreshTokenService)
    {
        _context = context;
        _config = config;
        _refreshTokenService = refreshTokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        var user = await _context.Usuarios
            .FirstOrDefaultAsync(x => x.Email == dto.Email && x.Activo);

        if (user == null) return Unauthorized();

        if (!PasswordHasher.VerifyPassword(
                dto.Password,
                Convert.FromBase64String(user.PasswordHash),
                Convert.FromBase64String(user.PasswordSalt)))
            return Unauthorized();

        var accessToken = JwtHelper.GenerateToken(user, _config);
        var refreshToken = RefreshTokenHelper.Generate();

        await _refreshTokenService.SaveAsync(refreshToken, user.Id);



        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(7),
            Path = "/"
        });

        return Ok(new { token = accessToken });
    }
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var oldToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(oldToken))
            return Unauthorized();

        var userId = await _refreshTokenService.ValidateAsync(oldToken);
        if (userId == null)
            return Unauthorized();

        var user = await _context.Usuarios.FindAsync(userId.Value);
        if (user == null || !user.Activo)
            return Unauthorized();

        await _refreshTokenService.RevokeAsync(oldToken);

        var newRefreshToken = RefreshTokenHelper.Generate();
        await _refreshTokenService.SaveAsync(newRefreshToken, user.Id);

        Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(7),
            Path = "/"
        });

        var accessToken = JwtHelper.GenerateToken(user, _config);

        return Ok(new { token = accessToken });
    }


    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (refreshToken != null)
        {
            await _refreshTokenService.RevokeAsync(refreshToken);
        }

        Response.Cookies.Delete("refreshToken", new CookieOptions
        {
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/"
        });

        return Ok();
    }






}


