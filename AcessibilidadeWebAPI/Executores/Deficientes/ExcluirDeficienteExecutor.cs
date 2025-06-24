using AcessibilidadeWebAPI.Repositorios.Deficientes;
using AcessibilidadeWebAPI.Requisicoes.Deficiente;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Deficientes
{
    public class ExcluirDeficienteExecutor : IRequestHandler<ExcluirDeficienteRequisicao>
    {
        private readonly IDeficienteRepositorio deficienteRepositorio;

        public ExcluirDeficienteExecutor(IDeficienteRepositorio DeficienteRepositorio)
        {
            this.deficienteRepositorio = deficienteRepositorio;
        }

        public Task Handle(ExcluirDeficienteRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.Deficiente Deficiente = deficienteRepositorio.ObterPorId(request.IdUsuario);

            deficienteRepositorio.Deletar(Deficiente);

            return Task.CompletedTask;
        }
    }
}
