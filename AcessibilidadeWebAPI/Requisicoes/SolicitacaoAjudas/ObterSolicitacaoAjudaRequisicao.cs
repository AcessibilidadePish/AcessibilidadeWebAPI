using AcessibilidadeWebAPI.Resultados.SolicitacaoAjudas;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas
{
    public class ObterSolicitacaoAjudaRequisicao : IRequest<ObterSolicitacaoAjudaResultado>
    { 
        public int IdSolicitacaoAjuda { get; set; }
    }
}
