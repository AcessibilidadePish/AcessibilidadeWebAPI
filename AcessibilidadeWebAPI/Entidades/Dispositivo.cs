namespace AcessibilidadeWebAPI.Entidades
{
    public partial class Dispositivo
    {
        public Dispositivo()
        {
            AvaliacaoLocals = new HashSet<AvaliacaoLocal>();
        }

        public int Id { get; set; }
        public string NumeroSerie { get; set; }
        public DateTime DataRegistro { get; set; }
        public int UsuarioProprietarioId { get; set; }
        public virtual Usuario UsuarioProprietarioNavigation { get; set; }
        public virtual ICollection<AvaliacaoLocal> AvaliacaoLocals { get; set; }
    }
} 