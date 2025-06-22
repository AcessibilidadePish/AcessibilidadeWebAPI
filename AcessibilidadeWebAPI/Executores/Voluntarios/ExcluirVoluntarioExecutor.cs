using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Repositorios.Voluntarios;
using AcessibilidadeWebAPI.Requisicoes.Usuarios;
using AcessibilidadeWebAPI.Requisicoes.Voluntario;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Voluntarios
{
    public class ExcluirVoluntarioExecutor : IRequestHandler<ExcluirVoluntarioRequisicao>
    {
        private readonly IVoluntarioRepositorio voluntarioRepositorio;

        public ExcluirVoluntarioExecutor(IVoluntarioRepositorio voluntarioRepositorio)
        {
            this.voluntarioRepositorio = voluntarioRepositorio;
        }

        public Task Handle(ExcluirVoluntarioRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.Voluntario voluntario = voluntarioRepositorio.ObterPorId(request.IdUsuario);

            voluntarioRepositorio.Deletar(voluntario);

            return Task.CompletedTask;
        }
    }
}
