using AcessibilidadeWebAPI.Dtos.Voluntario;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Repositorios.Voluntarios;
using AcessibilidadeWebAPI.Requisicoes.Voluntario;
using AcessibilidadeWebAPI.Resultados.Voluntario;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Voluntarios
{
    public class InserirVoluntarioExecutor : IRequestHandler<InserirVoluntarioRequisicao, InserirVoluntarioResultado>
    {
        private readonly IVoluntarioRepositorio voluntarioRepositorio;
        private readonly IMapper mapper;

        public InserirVoluntarioExecutor(IVoluntarioRepositorio voluntarioRepositorio, IMapper mapper)
        {
            this.voluntarioRepositorio = voluntarioRepositorio;
            this.mapper = mapper;
        }
        public Task<InserirVoluntarioResultado> Handle(InserirVoluntarioRequisicao request, CancellationToken cancellationToken)
        {
            Voluntario voluntario = mapper.Map<Voluntario>(request);
            Voluntario res = voluntarioRepositorio.Inserir(voluntario);

            return Task.FromResult(new InserirVoluntarioResultado()
            {
                Voluntario = mapper.Map<VoluntarioDto>(res),
            });
        }
    }
}
