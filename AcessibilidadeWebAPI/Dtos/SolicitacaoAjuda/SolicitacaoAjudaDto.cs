using AcessibilidadeWebAPI.Models.Auth;

namespace AcessibilidadeWebAPI.Dtos.SolicitacaoAjuda
{
    public class SolicitacaoAjudaDto
    {
        public int IdSolicitacaoAjuda { get; set; }
        public int DeficienteUsuarioId { get; set; }
        public string Descricao { get; set; }
        public StatusSolicitacao Status { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataResposta { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? EnderecoReferencia { get; set; }
    }
}
