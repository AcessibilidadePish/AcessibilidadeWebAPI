using AcessibilidadeWebAPI.Dtos.SolicitacaoAjuda;
using AcessibilidadeWebAPI.Repositorios.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Resultados.SolicitacaoAjudas;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.SolicitacaoAjudas
{
    public class ObterSolicitacaoAjudaExecutor : IRequestHandler<ObterSolicitacaoAjudaRequisicao, ObterSolicitacaoAjudaResultado>
    {
        public readonly ISolicitacaoAjudaRepositorio solicitacaoAjudaRepositorio; 
        private readonly IMapper mapper;


        public ObterSolicitacaoAjudaExecutor(ISolicitacaoAjudaRepositorio solicitacaoAjudaRepositorio, IMapper mapper)
        {
            this.solicitacaoAjudaRepositorio = solicitacaoAjudaRepositorio;
            this.mapper = mapper;
        }

        public Task<ObterSolicitacaoAjudaResultado> Handle(ObterSolicitacaoAjudaRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.SolicitacaoAjuda SolicitacaoAjuda = solicitacaoAjudaRepositorio.ObterPorId(request.IdSolicitacaoAjuda);

            SolicitacaoAjudaDto SolicitacaoAjudaDto = mapper.Map<SolicitacaoAjudaDto>(SolicitacaoAjuda);

            return Task.FromResult(new ObterSolicitacaoAjudaResultado()
            {
                SolicitacaoAjuda = SolicitacaoAjudaDto,
            });
        }
    }
}
