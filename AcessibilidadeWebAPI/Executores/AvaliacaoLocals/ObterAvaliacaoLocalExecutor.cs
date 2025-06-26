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
    public class ObterAvaliacaoLocalExecutor : IRequestHandler<ObterAvaliacaoLocalRequisicao, ObterAvaliacaoLocalResultado>
    {
        private readonly IMapper mapper;
        private readonly IAvaliacaoLocalRepositorio avaliacaoLocalRepositorio;

        public ObterAvaliacaoLocalExecutor(IMapper mapper, IAvaliacaoLocalRepositorio avaliacaoLocalRepositorio)
        {
            this.mapper = mapper;
            this.avaliacaoLocalRepositorio = avaliacaoLocalRepositorio;
        }
        public Task<ObterAvaliacaoLocalResultado> Handle(ObterAvaliacaoLocalRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.AvaliacaoLocal avaliacaoLocal = avaliacaoLocalRepositorio.ObterPorId(request.IdAvaliacaoLocal);

            AvaliacaoLocalDto avaliacaoLocalDto = mapper.Map<AvaliacaoLocalDto>(avaliacaoLocal);

            return Task.FromResult(new ObterAvaliacaoLocalResultado()
            {
                AvaliacaoLocal = avaliacaoLocalDto,
            });
        }
    }
}
