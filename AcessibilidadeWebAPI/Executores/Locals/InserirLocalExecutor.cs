using AcessibilidadeWebAPI.Dtos.Local;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Repositorios.Locals;
using AcessibilidadeWebAPI.Requisicoes.Locals;
using AcessibilidadeWebAPI.Resultados.Locals;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Locals
{
    public class InserirLocalExecutor : IRequestHandler<InserirLocalRequisicao, InserirLocalResultado>
    {
        private readonly ILocalRepositorio localRepositorio;
        private readonly IMapper mapper;

        public InserirLocalExecutor(ILocalRepositorio localRepositorio, IMapper mapper)
        {
            this.localRepositorio = localRepositorio;
            this.mapper = mapper;
        }
        public Task<InserirLocalResultado> Handle(InserirLocalRequisicao request, CancellationToken cancellationToken)
        {
            Local Local = mapper.Map<Local>(request);
            Local res = localRepositorio.Inserir(Local);

            return Task.FromResult(new InserirLocalResultado()
            {
                Local = mapper.Map<LocalDto>(res),
            });
        }
    }
}
