using AcessibilidadeWebAPI.Repositorios.AvaliacaoAvaliacaoLocals;
using AcessibilidadeWebAPI.Repositorios.Locals;
using AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.AvaliacaoLocals
{
    public class ExcluirAvaliacaoLocalExecutor : IRequestHandler<ExcluirAvaliacaoLocalRequisicao>
    {
        private readonly IAvaliacaoLocalRepositorio avaliacaoLocalRepositorio;

        public ExcluirAvaliacaoLocalExecutor(IAvaliacaoLocalRepositorio avaliacaoLocalRepositorio)
        {
            this.avaliacaoLocalRepositorio = avaliacaoLocalRepositorio;
        }

        public Task Handle(ExcluirAvaliacaoLocalRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.AvaliacaoLocal avaliacaoLocal = avaliacaoLocalRepositorio.ObterPorId(request.IdAvaliacaoLocal);

            avaliacaoLocalRepositorio.Deletar(avaliacaoLocal);

            return Task.CompletedTask;
        }
    }
}
