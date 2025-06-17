namespace AcessibilidadeWebAPI.Models.Usuarios
{
    public class InserirUsuarioInput
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Telefone { get; set; }
        public bool EhDeficiente { get; set; }
    }
}
