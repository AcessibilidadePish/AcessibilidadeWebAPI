using AcessibilidadeWebAPI.Resultados.SolicitacaoAjudas;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas
{
    public class ListarSolicitacaoAjudaRequisicao : IRequest<ListarSolicitacaoAjudaResultado>
    {
        public int IdUsuario { get; set; }

    }
}
