namespace AcessibilidadeWebAPI.Models.Auth
{
    public class SolicitacoesDisponiveisOutput
    {
        public SolicitacaoComDistancia[] Solicitacoes { get; set; }
    }

    public class SolicitacaoComDistancia
    {
        public int IdSolicitacaoAjuda { get; set; }
        public int DeficienteUsuarioId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? EnderecoReferencia { get; set; }
        public double DistanciaKm { get; set; }
        public string? StatusDescricao => DistanciaKm == -1 ? "Sem localização" : $"{DistanciaKm:F1} km";
    }
} 