namespace AcessibilidadeWebAPI.Models.Usuarios
{
    public class InserirUsuarioInput
    {
        public int IdUsuario {  get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Telefone { get; set; }
        public bool ehDeficiente { get; set; }
    }
}
