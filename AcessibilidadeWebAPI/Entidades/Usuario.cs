namespace AcessibilidadeWebAPI.Entidades
{
    public partial class Usuario
    {
        public Usuario()
        {
            Voluntarios = new HashSet<Voluntario>();
            Deficientes = new HashSet<Deficiente>();
        }
        public int IdUsuario { get; set; }
        public string Nome { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string Telefone { get; set; }
        public string Senha { get; set; }

        public virtual ICollection<Voluntario> Voluntarios { get; set; }

        public virtual ICollection<Deficiente> Deficientes { get; set; }
    }
}
