namespace AcessibilidadeWebAPI.Dtos.Local
{
    public class LocalDto
    {
        public int IdLocal { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public string Descricao { get; set; }
        public int AvaliacaoAcessibilidade { get; set; }
    }
}
