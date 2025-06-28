namespace AcessibilidadeWebAPI.Models.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public UsuarioInfo Usuario { get; set; }
    }

    public class UsuarioInfo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string? TipoUsuario { get; set; }
        public VoluntarioInfo? Voluntario { get; set; }
        public DeficienteInfo? Deficiente { get; set; }
    }

    public class VoluntarioInfo
    {
        public bool Disponivel { get; set; }
        public decimal Avaliacao { get; set; }
    }

    public class DeficienteInfo
    {
        public int TipoDeficiencia { get; set; }
        public string? TipoDeficienciaDescricao { get; set; }
    }
} 