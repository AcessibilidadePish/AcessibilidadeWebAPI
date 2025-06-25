using AcessibilidadeWebAPI.Dtos.Local;
using AcessibilidadeWebAPI.Repositorios.Locals;
using AcessibilidadeWebAPI.Requisicoes.Locals;
using AcessibilidadeWebAPI.Resultados.Locals;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Locals
{
    public class ObterLocalExecutor : IRequestHandler<ObterLocalRequisicao, ObterLocalResultado>
    {
        private readonly IMapper mapper;
        private readonly ILocalRepositorio localRepositorio;

        public ObterLocalExecutor(IMapper mapper, ILocalRepositorio localRepositorio)
        {
            this.mapper = mapper;
            this.localRepositorio = localRepositorio;
        }
        public Task<ObterLocalResultado> Handle(ObterLocalRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.Local local = localRepositorio.ObterPorId(request.IdLocal);

            var localDto = mapper.Map<LocalDto>(local);

            return Task.FromResult(new ObterLocalResultado()
            {
                Local = localDto,
            });
        }
    }
}
