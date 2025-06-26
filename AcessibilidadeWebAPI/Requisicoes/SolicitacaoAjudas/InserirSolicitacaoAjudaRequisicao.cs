using AcessibilidadeWebAPI.Resultados.SolicitacaoAjudas;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas
{
    public class InserirSolicitacaoAjudaRequisicao : IRequest<InserirSolicitacaoAjudaResultado>
    {
        public int IdUsuario { get; set; }
        public string Descricao { get; set; }
        public int Status { get; set; }
        public DateTimeOffset DataSolicitacao { get; set; }
    }
}
