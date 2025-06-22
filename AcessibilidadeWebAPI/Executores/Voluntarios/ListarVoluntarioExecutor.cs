using AcessibilidadeWebAPI.Dtos.Usuario;
using AcessibilidadeWebAPI.Dtos.Voluntario;
using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Repositorios.Voluntarios;
using AcessibilidadeWebAPI.Requisicoes.Voluntario;
using AcessibilidadeWebAPI.Resultados.Usuarios;
using AcessibilidadeWebAPI.Resultados.Voluntario;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Voluntarios
{
    public class ListarVoluntarioExecutor : IRequestHandler<ListarVoluntarioRequisicao, ListarVoluntarioResultado>
    {
        private readonly IMapper mapper;
        private readonly IVoluntarioRepositorio voluntarioRepositorio;

        public ListarVoluntarioExecutor(IMapper mapper, IVoluntarioRepositorio voluntarioRepositorio)
        {
            this.mapper = mapper;
            this.voluntarioRepositorio = voluntarioRepositorio;
        }
        public Task<ListarVoluntarioResultado> Handle(ListarVoluntarioRequisicao request, CancellationToken cancellationToken)
        {
            IQueryable<Entidades.Voluntario> arrVoluntarios = voluntarioRepositorio.Listar();

            VoluntarioDto[] arrVoluntariosDto = mapper.ProjectTo<VoluntarioDto>(arrVoluntarios).ToArray();

            return Task.FromResult(new ListarVoluntarioResultado()
            {
                ArrVoluntario = arrVoluntariosDto,
            });
        }
    }
}
