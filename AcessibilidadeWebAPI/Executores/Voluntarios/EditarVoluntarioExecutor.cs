using AcessibilidadeWebAPI.Repositorios.Voluntarios;
using AcessibilidadeWebAPI.Requisicoes.Voluntario;
using AcessibilidadeWebAPI.Resultados.Voluntario;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Voluntarios
{
    public class EditarVoluntarioExecutor : IRequestHandler<EditarVoluntarioRequisicao, EditarVoluntarioResultado>
    {
        private readonly IVoluntarioRepositorio voluntarioRepositorio;
        private readonly IMapper mapper;

        public EditarVoluntarioExecutor(IVoluntarioRepositorio voluntarioRepositorio, IMapper mapper)
        {
            this.voluntarioRepositorio = voluntarioRepositorio;
            this.mapper = mapper;
        }
        public Task<EditarVoluntarioResultado> Handle(EditarVoluntarioRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.Voluntario Voluntario = voluntarioRepositorio.ObterPorId(request.IdUsuario);

            mapper.Map(request, Voluntario);
            voluntarioRepositorio.Editar(Voluntario);

            return Task.FromResult(new EditarVoluntarioResultado()
            {
                IdUsuario = request.IdUsuario,
            });
        }
    }
}
