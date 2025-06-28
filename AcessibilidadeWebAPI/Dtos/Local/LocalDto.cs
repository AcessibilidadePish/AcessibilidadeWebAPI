namespace AcessibilidadeWebAPI.Dtos.Local
{
    public class LocalDto
    {
        public int IdLocal { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Descricao { get; set; }
        public int AvaliacaoAcessibilidade { get; set; }
    }
}
