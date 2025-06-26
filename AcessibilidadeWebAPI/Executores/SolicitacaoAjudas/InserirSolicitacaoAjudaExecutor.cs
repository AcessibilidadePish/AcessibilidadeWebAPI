using AcessibilidadeWebAPI.Dtos.SolicitacaoAjuda;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Repositorios.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Resultados.SolicitacaoAjudas;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.SolicitacaoAjudas
{
    public class InserirSolicitacaoAjudaExecutor : IRequestHandler<InserirSolicitacaoAjudaRequisicao, InserirSolicitacaoAjudaResultado>
    {
        public readonly ISolicitacaoAjudaRepositorio solicitacaoAjudaRepositorio;
        private readonly IMapper mapper;


        public InserirSolicitacaoAjudaExecutor(ISolicitacaoAjudaRepositorio solicitacaoAjudaRepositorio, IMapper mapper)
        {
            this.solicitacaoAjudaRepositorio = solicitacaoAjudaRepositorio;
            this.mapper = mapper;
        }
        public Task<InserirSolicitacaoAjudaResultado> Handle(InserirSolicitacaoAjudaRequisicao request, CancellationToken cancellationToken)
        {
            SolicitacaoAjuda SolicitacaoAjuda = mapper.Map<SolicitacaoAjuda>(request);
            SolicitacaoAjuda res = solicitacaoAjudaRepositorio.Inserir(SolicitacaoAjuda);

            return Task.FromResult(new InserirSolicitacaoAjudaResultado()
            {
                SolicitacaoAjuda = mapper.Map<SolicitacaoAjudaDto>(res),
            });
        }
    }
}
