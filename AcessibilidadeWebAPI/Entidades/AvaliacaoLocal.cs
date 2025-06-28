namespace AcessibilidadeWebAPI.Entidades
{
    public partial class AvaliacaoLocal
    {
        public int Id { get; set; }
        public int LocalId { get; set; }
        public int DispositivoId { get; set; }
        public bool Acessivel { get; set; }
        public string Observacoes { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual Local LocalNavigation { get; set; }
        public virtual Dispositivo DispositivoNavigation { get; set; }
    }
}
