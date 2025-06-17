using AcessibilidadeWebAPI.Dtos.Usuario;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Requisicoes.Usuarios;
using AcessibilidadeWebAPI.Resultados.Usuarios;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Usuarios
{
    public class InserirUsuarioExecutor : IRequestHandler<InserirUsuarioRequisicao, InserirUsuarioResultado>
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IMapper mapper;

        public InserirUsuarioExecutor(IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.mapper = mapper;
        }
        public Task<InserirUsuarioResultado> Handle(InserirUsuarioRequisicao request, CancellationToken cancellationToken)
        {
            Usuario usuario = mapper.Map<Usuario>(request);
            Usuario res = usuarioRepositorio.Inserir(usuario);

            return Task.FromResult(new InserirUsuarioResultado()
            {
                Usuario = mapper.Map<UsuarioDto>(res),
            });
        }
    }
}
