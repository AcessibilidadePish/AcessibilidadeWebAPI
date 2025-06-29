using AcessibilidadeWebAPI.Dtos.Assistencia;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Repositorios.Assistencias;
using AcessibilidadeWebAPI.Repositorios.Assistencias;
using AcessibilidadeWebAPI.Repositorios.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Requisicoes.Assistencias;
using AcessibilidadeWebAPI.Resultados.Assistencias;
using AcessibilidadeWebAPI.Resultados.Assistencias;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Assistencias
{
    public class InserirAssistenciaExecutor : IRequestHandler<InserirAssistenciaRequisicao, InserirAssistenciaResultado>
    {
        private readonly IMapper mapper;
        private readonly IAssistenciaRepositorio assistenciaRepositorio;
        private readonly ISolicitacaoAjudaRepositorio solicitacaoAjudaRepositorio;

        public InserirAssistenciaExecutor(IMapper mapper, IAssistenciaRepositorio assistenciaRepositorio, ISolicitacaoAjudaRepositorio solicitacaoAjudaRepositorio)
        {
            this.mapper = mapper;
            this.assistenciaRepositorio = assistenciaRepositorio;
            this.solicitacaoAjudaRepositorio = solicitacaoAjudaRepositorio;
        }
        public Task<InserirAssistenciaResultado> Handle(InserirAssistenciaRequisicao request, CancellationToken cancellationToken)
        {
            var ajuda = solicitacaoAjudaRepositorio.ObterPorId(request.IdSolicitacaoAjuda);

            Assistencia assistencia = mapper.Map<Assistencia>(request);
            assistencia.DeficienteIdUsuario = ajuda.DeficienteUsuarioId;
            Assistencia res = assistenciaRepositorio.Inserir(assistencia);
            ajuda.Status = Models.Auth.StatusSolicitacao.Aceita;
            ajuda.DataResposta = DateTime.Now;
            solicitacaoAjudaRepositorio.Editar(ajuda);

            return Task.FromResult(new InserirAssistenciaResultado()
            {
                Assistencia = mapper.Map<AssistenciaDto>(res),
            });
        }
    }
}
