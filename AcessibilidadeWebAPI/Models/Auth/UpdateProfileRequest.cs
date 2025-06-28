namespace AcessibilidadeWebAPI.Models.Auth
{
    public class UpdateProfileRequest
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? NovaSenha { get; set; }
    }
} 