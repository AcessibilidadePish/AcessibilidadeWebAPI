namespace AcessibilidadeWebAPI.Entidades
{
    public partial class Local
    {
        public int IdLocal { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public string Descricao { get; set; }
        public int AvaliacaoAcessibilidade { get; set; }
    }
}
