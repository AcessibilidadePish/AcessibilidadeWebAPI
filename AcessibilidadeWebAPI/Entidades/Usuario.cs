namespace AcessibilidadeWebAPI.Entidades
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string Telefone { get; set; }
        public bool EhDeficiente { get; set; }
    }
}
