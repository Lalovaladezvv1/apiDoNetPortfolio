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

    public AuthController(
        FreeSqlDb2016559Context context,
        IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login(
        LoginRequestDto dto)
    {

        var user = await _context.Usuarios
            .FirstOrDefaultAsync(x => x.Email == dto.Email && x.Activo);

        if (user == null)
            return Unauthorized("Credenciales inválidas");

        var valid = PasswordHasher.VerifyPassword(
            dto.Password,
            Convert.FromBase64String(user.PasswordHash),
            Convert.FromBase64String(user.PasswordSalt));

        if (!valid)
            return Unauthorized("Credenciales inválidas");

        var token = JwtHelper.GenerateToken(user, _config);

        return Ok(new LoginResponseDto { Token = token });
    }
}


