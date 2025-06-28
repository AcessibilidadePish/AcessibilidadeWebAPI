namespace AcessibilidadeWebAPI.Models.Auth
{
    public class CriarSolicitacaoRequest
    {
        public string Descricao { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? EnderecoReferencia { get; set; }
    }
} 