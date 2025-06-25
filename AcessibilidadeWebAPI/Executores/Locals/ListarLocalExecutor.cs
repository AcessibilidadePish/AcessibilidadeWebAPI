using AcessibilidadeWebAPI.Dtos.Local;
using AcessibilidadeWebAPI.Repositorios.Locals;
using AcessibilidadeWebAPI.Requisicoes.Locals;
using AcessibilidadeWebAPI.Resultados.Locals;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Locals
{
    public class ListarLocalExecutor : IRequestHandler<ListarLocalRequisicao, ListarLocalResultado>
    {
        private readonly IMapper mapper;
        private readonly ILocalRepositorio localRepositorio;

        public ListarLocalExecutor(IMapper mapper, ILocalRepositorio localRepositorio)
        {
            this.mapper = mapper;
            this.localRepositorio = localRepositorio;
        }
        public Task<ListarLocalResultado> Handle(ListarLocalRequisicao request, CancellationToken cancellationToken)
        {
            IQueryable<Entidades.Local> arrLocals = localRepositorio.Listar();

            LocalDto[] arrLocalsDto = mapper.ProjectTo<LocalDto>(arrLocals).ToArray();

            return Task.FromResult(new ListarLocalResultado()
            {
                ArrLocal = arrLocalsDto,
            });
        }
    }
}
