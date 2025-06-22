namespace AcessibilidadeWebAPI.Dtos.Usuario
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string Telefone { get; set; }
        public string Senha { get; set; }
    }
}
