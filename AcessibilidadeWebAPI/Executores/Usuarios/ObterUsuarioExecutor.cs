using AcessibilidadeWebAPI.Dtos.Usuario;
using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Requisicoes.Usuarios;
using AcessibilidadeWebAPI.Resultados.Usuarios;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AcessibilidadeWebAPI.Executores.Usuarios
{
    public class ObterUsuarioExecutor : IRequestHandler<ObterUsuarioRequisicao, ObterUsuarioResultado>
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IMapper mapper;

        public ObterUsuarioExecutor(IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.mapper = mapper;
        }

        public Task<ObterUsuarioResultado> Handle(ObterUsuarioRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.Usuario usuario = usuarioRepositorio.ObterPorId(request.IdUsuario);

            var usuarioDto = mapper.Map<UsuarioDto>(usuario);

            return (new ObterUsuarioResultado()
            {
                Usuario = usuarioDto,
            });
        }
    }
}
