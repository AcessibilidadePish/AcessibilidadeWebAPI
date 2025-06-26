using AcessibilidadeWebAPI.Repositorios.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.SolicitacaoAjudas
{
    public class ExcluirSolicitacaoAjudaExecutor : IRequestHandler<ExcluirSolicitacaoAjudaRequisicao>
    {
        public readonly ISolicitacaoAjudaRepositorio solicitacaoAjudaRepositorio;

        public ExcluirSolicitacaoAjudaExecutor(ISolicitacaoAjudaRepositorio solicitacaoAjudaRepositorio)
        {
            this.solicitacaoAjudaRepositorio = solicitacaoAjudaRepositorio;
        }

        public Task Handle(ExcluirSolicitacaoAjudaRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.SolicitacaoAjuda SolicitacaoAjuda = solicitacaoAjudaRepositorio.ObterPorId(request.IdSolicitacaoAjuda);

            solicitacaoAjudaRepositorio.Deletar(SolicitacaoAjuda);

            return Task.CompletedTask;
        }
    }
}
