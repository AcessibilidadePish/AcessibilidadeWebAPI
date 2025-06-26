using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas
{
    public class ExcluirSolicitacaoAjudaRequisicao : IRequest
    {
        public int IdSolicitacaoAjuda { get; set; }

    }
}
