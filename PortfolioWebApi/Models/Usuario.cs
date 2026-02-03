public class Usuario
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
    public string Rol { get; set; } = "User";
    public bool Activo { get; set; } = true;
}
