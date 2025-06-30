using AcessibilidadeWebAPI.Dtos.Assistencia;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Repositorios.Assistencias;
using AcessibilidadeWebAPI.Repositorios.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Requisicoes.Assistencias;
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
        private readonly AzureMqttPushService mqttPushService;
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private const string IOT_USER_EMAIL = "dispositivo.iot@acessibilidade.com";

        public InserirAssistenciaExecutor(IMapper mapper, IAssistenciaRepositorio assistenciaRepositorio, ISolicitacaoAjudaRepositorio solicitacaoAjudaRepositorio,
            IUsuarioRepositorio usuarioRepositorio)
        {
            this.mapper = mapper;
            this.assistenciaRepositorio = assistenciaRepositorio;
            this.solicitacaoAjudaRepositorio = solicitacaoAjudaRepositorio;
            mqttPushService = new AzureMqttPushService();
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<InserirAssistenciaResultado> Handle(InserirAssistenciaRequisicao request, CancellationToken cancellationToken)
        {
            SolicitacaoAjuda ajuda = solicitacaoAjudaRepositorio.ObterPorId(request.IdSolicitacaoAjuda);

            Assistencia assistencia = mapper.Map<Assistencia>(request);
            assistencia.DeficienteIdUsuario = ajuda.DeficienteUsuarioId;
            Assistencia res = assistenciaRepositorio.Inserir(assistencia);
            ajuda.Status = Models.Auth.StatusSolicitacao.Aceita;
            ajuda.DataResposta = DateTime.Now;
            solicitacaoAjudaRepositorio.Editar(ajuda);

            Usuario usuarioSolicitador = usuarioRepositorio.ObterPorId(ajuda.DeficienteUsuarioId);

            if (usuarioSolicitador.Email == IOT_USER_EMAIL)
            {
                await mqttPushService.EnviarAckParaDispositivoAsync();
            }

            return (new InserirAssistenciaResultado()
            {
                Assistencia = mapper.Map<AssistenciaDto>(res),
            });
        }
    }
}