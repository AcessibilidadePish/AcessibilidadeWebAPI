namespace AcessibilidadeWebAPI.Models.Locals
{
    public class InserirLocalInput
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Descricao { get; set; }
        public int AvaliacaoAcessibilidade { get; set; }
    }
}
