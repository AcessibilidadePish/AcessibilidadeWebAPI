using AcessibilidadeWebAPI.Resultados.SolicitacaoAjudas;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas
{
    public class InserirSolicitacaoAjudaRequisicao : IRequest<InserirSolicitacaoAjudaResultado>
    {
        public int IdUsuario { get; set; }
        public string Descricao { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? EnderecoReferencia { get; set; }
    }
}
