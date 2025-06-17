using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Requisicoes.Usuarios;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Usuarios
{
    public class ExcluirUsuarioExecutor : IRequestHandler<ExcluirUsuarioRequisicao>
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;

        public ExcluirUsuarioExecutor(IUsuarioRepositorio usuarioRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public Task Handle(ExcluirUsuarioRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.Usuario usuario = usuarioRepositorio.ObterPorId(request.IdUsuario);

            usuarioRepositorio.Deletar(usuario);

            return Task.CompletedTask;
        }
    }
}
