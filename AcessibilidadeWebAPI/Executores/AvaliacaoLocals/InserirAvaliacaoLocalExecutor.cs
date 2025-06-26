using AcessibilidadeWebAPI.Dtos.AvaliacaoLocal;
using AcessibilidadeWebAPI.Dtos.Local;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Repositorios.AvaliacaoAvaliacaoLocals;
using AcessibilidadeWebAPI.Repositorios.Locals;
using AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals;
using AcessibilidadeWebAPI.Resultados.AvaliacaoLocals;
using AcessibilidadeWebAPI.Resultados.Locals;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.AvaliacaoLocals
{
    public class InserirAvaliacaoLocalExecutor : IRequestHandler<InserirAvaliacaoLocalRequisicao, InserirAvaliacaoLocalResultado>
    {
        private readonly IAvaliacaoLocalRepositorio avaliacaoLocalRepositorio;
        private readonly IMapper mapper;

        public InserirAvaliacaoLocalExecutor(IMapper mapper, IAvaliacaoLocalRepositorio avaliacaoLocalRepositorio)
        {
            this.mapper = mapper;
            this.avaliacaoLocalRepositorio = avaliacaoLocalRepositorio;
        }

        public Task<InserirAvaliacaoLocalResultado> Handle(InserirAvaliacaoLocalRequisicao request, CancellationToken cancellationToken)
        {
            AvaliacaoLocal Local = mapper.Map<AvaliacaoLocal>(request);
            AvaliacaoLocal res = avaliacaoLocalRepositorio.Inserir(Local);

            return Task.FromResult(new InserirAvaliacaoLocalResultado()
            {
                AvaliacaoLocal = mapper.Map<AvaliacaoLocalDto>(res),
            });
        }
    }
}
