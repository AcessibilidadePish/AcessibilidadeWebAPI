using AcessibilidadeWebAPI.Repositorios.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Resultados.SolicitacaoAjudas;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.SolicitacaoAjudas
{
    public class EditarSolicitacaoAjudaExecutor : IRequestHandler<EditarSolicitacaoAjudaRequisicao, EditarSolicitacaoAjudaResultado>
    {
        public readonly ISolicitacaoAjudaRepositorio solicitacaoAjudaRepositorio;
        private readonly IMapper mapper;


        public EditarSolicitacaoAjudaExecutor(ISolicitacaoAjudaRepositorio solicitacaoAjudaRepositorio, IMapper mapper)
        {
            this.solicitacaoAjudaRepositorio = solicitacaoAjudaRepositorio;
            this.mapper = mapper;
        }
        public Task<EditarSolicitacaoAjudaResultado> Handle(EditarSolicitacaoAjudaRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.SolicitacaoAjuda SolicitacaoAjuda = solicitacaoAjudaRepositorio.ObterPorId(request.IdSolicitacaoAjuda);

            mapper.Map(request, SolicitacaoAjuda);
            solicitacaoAjudaRepositorio.Editar(SolicitacaoAjuda);

            return Task.FromResult(new EditarSolicitacaoAjudaResultado()
            {
                IdSolicitacaoAjuda = request.IdSolicitacaoAjuda,
            });
        }
    }
}
