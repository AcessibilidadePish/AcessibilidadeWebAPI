using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Requisicoes.Usuarios;
using AcessibilidadeWebAPI.Resultados.Usuarios;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Usuarios
{
    public class EditarUsuarioExecutor : IRequestHandler<EditarUsuarioRequisicao, EditarUsuarioResultado>
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IMapper mapper;

        public EditarUsuarioExecutor(IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.mapper = mapper;
        }
        public Task<EditarUsuarioResultado> Handle(EditarUsuarioRequisicao request, CancellationToken cancellationToken)
        {
            var usuario = usuarioRepositorio.ObterPorId(request.IdUsuario);

            mapper.Map(request, usuario);
            usuarioRepositorio.Editar(usuario);

            return Task.FromResult(new EditarUsuarioResultado()
            {
                IdUsuario = request.IdUsuario,
            });
        }
    }
}
