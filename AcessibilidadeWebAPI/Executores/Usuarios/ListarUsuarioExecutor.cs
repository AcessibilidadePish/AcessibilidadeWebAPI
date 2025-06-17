using AcessibilidadeWebAPI.Dtos.Usuario;
using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Requisicoes.Usuarios;
using AcessibilidadeWebAPI.Resultados.Usuarios;
using AutoMapper;
using MediatR;
using Microsoft.Azure.Amqp.Encoding;

namespace AcessibilidadeWebAPI.Executores.Usuarios
{
    public class ListarUsuarioExecutor : IRequestHandler<ListarUsuarioRequisicao, ListarUsuarioResultado>
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IMapper mapper;

        public ListarUsuarioExecutor(IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.mapper = mapper;
        }

        public Task<ListarUsuarioResultado> Handle(ListarUsuarioRequisicao request, CancellationToken cancellationToken)
        {
            IQueryable<Entidades.Usuario> arrUsuarios = usuarioRepositorio.Listar();

            UsuarioDto[] arrUsuarioDto = mapper.ProjectTo<UsuarioDto>(arrUsuarios).ToArray();

            return Task.FromResult(new ListarUsuarioResultado()
            {
                ArrUsuario = arrUsuarioDto,
            });
        }
    }
}
