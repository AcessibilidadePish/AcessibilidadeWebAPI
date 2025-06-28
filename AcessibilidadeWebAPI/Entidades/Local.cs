namespace AcessibilidadeWebAPI.Entidades
{
    public partial class Local
    {
        public Local()
        {
            AvaliacaoLocals = new HashSet<AvaliacaoLocal>();
        }
        public int IdLocal { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Descricao { get; set; }
        public int AvaliacaoAcessibilidade { get; set; }
        public virtual ICollection<AvaliacaoLocal> AvaliacaoLocals { get; set; }
    }
}
