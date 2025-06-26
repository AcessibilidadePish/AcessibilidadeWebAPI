using AcessibilidadeWebAPI.Resultados.SolicitacaoAjudas;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas
{
    public class EditarSolicitacaoAjudaRequisicao : IRequest<EditarSolicitacaoAjudaResultado>
    {
        public int IdSolicitacaoAjuda { get; set; }
        public int IdUsuario { get; set; }
        public string Descricao { get; set; }
        public int Status { get; set; }
        public DateTimeOffset DataSolicitacao { get; set; }
        public DateTimeOffset DataResposta { get; set; }
    }
}
