using AcessibilidadeWebAPI.Repositorios.Locals;
using AcessibilidadeWebAPI.Requisicoes.Locals;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Locals
{
    public class ExcluirLocalExecutor : IRequestHandler<ExcluirLocalRequisicao>
    {
        private readonly ILocalRepositorio localRepositorio;

        public ExcluirLocalExecutor(ILocalRepositorio localRepositorio)
        {
            this.localRepositorio = localRepositorio;
        }
        public Task Handle(ExcluirLocalRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.Local local = localRepositorio.ObterPorId(request.IdLocal);

            localRepositorio.Deletar(local);

            return Task.CompletedTask;
        }
    }
}
