using AcessibilidadeWebAPI.Dtos.SolicitacaoAjuda;
using AcessibilidadeWebAPI.Repositorios.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Resultados.SolicitacaoAjudas;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.SolicitacaoAjudas
{
    public class ListarSolicitacaoAjudaExecutor : IRequestHandler<ListarSolicitacaoAjudaRequisicao, ListarSolicitacaoAjudaResultado>
    {
        public readonly ISolicitacaoAjudaRepositorio solicitacaoAjudaRepositorio;
        private readonly IMapper mapper;


        public ListarSolicitacaoAjudaExecutor(ISolicitacaoAjudaRepositorio solicitacaoAjudaRepositorio, IMapper mapper)
        {
            this.solicitacaoAjudaRepositorio = solicitacaoAjudaRepositorio;
            this.mapper = mapper;
        }
        public Task<ListarSolicitacaoAjudaResultado> Handle(ListarSolicitacaoAjudaRequisicao request, CancellationToken cancellationToken)
        {
            IQueryable<Entidades.SolicitacaoAjuda> arrSolicitacaoAjudas = solicitacaoAjudaRepositorio.Listar();

            SolicitacaoAjudaDto[] arrSolicitacaoAjudasDto = mapper.ProjectTo<SolicitacaoAjudaDto>(arrSolicitacaoAjudas).ToArray();

            return Task.FromResult(new ListarSolicitacaoAjudaResultado()
            {
                ArrSolicitacaoAjuda = arrSolicitacaoAjudasDto,
            });
        }
    }
}
