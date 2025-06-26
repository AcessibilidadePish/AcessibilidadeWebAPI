using AcessibilidadeWebAPI.Dtos.AvaliacaoLocal;
using AcessibilidadeWebAPI.Dtos.Local;
using AcessibilidadeWebAPI.Repositorios.AvaliacaoAvaliacaoLocals;
using AcessibilidadeWebAPI.Repositorios.Locals;
using AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals;
using AcessibilidadeWebAPI.Resultados.AvaliacaoLocals;
using AcessibilidadeWebAPI.Resultados.Locals;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.AvaliacaoLocals
{
    public class ListarAvaliacaoLocalExecutor : IRequestHandler<ListarAvaliacaoLocalRequisicao, ListarAvaliacaoLocalResultado>
    {
        private readonly IAvaliacaoLocalRepositorio avaliacaoLocalRepositorio;
        private readonly IMapper mapper;

        public ListarAvaliacaoLocalExecutor(IAvaliacaoLocalRepositorio avaliacaoLocalRepositorio, IMapper mapper)
        {
            this.avaliacaoLocalRepositorio = avaliacaoLocalRepositorio;
            this.mapper = mapper;
        }

        public Task<ListarAvaliacaoLocalResultado> Handle(ListarAvaliacaoLocalRequisicao request, CancellationToken cancellationToken)
        {
            IQueryable<Entidades.AvaliacaoLocal> arrAvaliacaoLocals = avaliacaoLocalRepositorio.Listar(a => a.IdLocal == request.IdLocal);

            AvaliacaoLocalDto[] arrAvaliacaoLocalsDto = mapper.ProjectTo<AvaliacaoLocalDto>(arrAvaliacaoLocals).ToArray();

            return Task.FromResult(new ListarAvaliacaoLocalResultado()
            {
                ArrAvaliacaoLocal = arrAvaliacaoLocalsDto,
            });
        }
    }
}
