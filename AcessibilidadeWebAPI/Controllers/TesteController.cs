using AcessibilidadeWebAPI.Dtos.Testes;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Models.Auth;
using AcessibilidadeWebAPI.Repositorios.Deficientes;
using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Resultados.SolicitacaoAjudas;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AcessibilidadeWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ControllerBase
    {
        private readonly ILogger<TesteController> _logger;
        private readonly AzureMqttPushService mqttPush;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IDeficienteRepositorio _deficienteRepositorio;
        private readonly IMediator _mediator;

        // Constantes para o usuário IoT
        private const string IOT_USER_NAME = "dispositivoIoT";
        private const string IOT_USER_EMAIL = "dispositivo.iot@acessibilidade.com";
        private const string IOT_USER_TELEFONE = "(11) 99999-0000";
        private const string IOT_USER_SENHA = "IoTDevice123!";

        public TesteController(
            ILogger<TesteController> logger, 
            AzureMqttPushService mqttPush,
            IUsuarioRepositorio usuarioRepositorio,
            IDeficienteRepositorio deficienteRepositorio,
            IMediator mediator)
        {
            _logger = logger;
            this.mqttPush = mqttPush;
            _usuarioRepositorio = usuarioRepositorio;
            _deficienteRepositorio = deficienteRepositorio;
            _mediator = mediator;
        }

        [HttpPost("TestePost")]
        public async Task<IActionResult> TestePost(TesteDto latLngFromEsp)
        {
            try
            {
                _logger.LogInformation("TestePost called with: {lat}, {lng}", latLngFromEsp.lat, latLngFromEsp.lng);
                
                // 1. Verificar/Criar usuário IoT
                Usuario usuarioIoT = await VerificarOuCriarUsuarioIoT();
                _logger.LogInformation("Usuario IoT verificado/criado: ID {UserId}", usuarioIoT.IdUsuario);

                // 2. Criar solicitação de ajuda para o usuário IoT
                var solicitacaoId = await CriarSolicitacaoAjudaIoT(usuarioIoT.IdUsuario, latLngFromEsp);
                _logger.LogInformation("Solicitação de ajuda criada: ID {SolicitacaoId}", solicitacaoId);

                // 3. Enviar ACK via MQTT
                _logger.LogInformation("Sending ACK to device via MQTT...");    
                //await mqttPush.EnviarAckParaDispositivoAsync();
                _logger.LogInformation("ACK sent successfully.");

                return Ok(new { 
                    message = "Solicitação de ajuda IoT criada com sucesso",
                    usuarioId = usuarioIoT.IdUsuario,
                    solicitacaoId = solicitacaoId,
                    latitude = latLngFromEsp.lat,
                    longitude = latLngFromEsp.lng
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar TestePost");
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Verifica se o usuário IoT existe, se não existir, cria ele
        /// </summary>
        /// <returns>Usuario IoT</returns>
        private async Task<Usuario> VerificarOuCriarUsuarioIoT()
        {
            // Verificar se usuário já existe
            Usuario? usuarioExistente = await _usuarioRepositorio.ObterUsuarioPorEmail(IOT_USER_EMAIL);
            
            if (usuarioExistente != null)
            {
                _logger.LogInformation("Usuario IoT já existe: {Email}", IOT_USER_EMAIL);
                return usuarioExistente;
            }

            _logger.LogInformation("Usuario IoT não existe, criando novo: {Email}", IOT_USER_EMAIL);

            // Criar novo usuário IoT
            Usuario novoUsuario = new Usuario
            {
                Nome = IOT_USER_NAME,
                Email = IOT_USER_EMAIL,
                Telefone = IOT_USER_TELEFONE,
                Senha = IOT_USER_SENHA
            };

            Usuario usuarioCriado = await _usuarioRepositorio.InserirUsuario(novoUsuario);

            // Criar perfil de deficiente para o usuário IoT
            Deficiente deficiente = new Deficiente
            {
                IdUsuario = usuarioCriado.IdUsuario,
                TipoDeficiencia = TipoDeficiencia.Fisica // Assumindo deficiência física para dispositivos IoT
            };

            _deficienteRepositorio.Inserir(deficiente);
            _logger.LogInformation("Perfil de deficiente criado para usuario IoT");

            return usuarioCriado;
        }

        /// <summary>
        /// Cria uma solicitação de ajuda para o usuário IoT
        /// </summary>
        /// <param name="usuarioId">ID do usuário IoT</param>
        /// <param name="dados">Dados do ESP32</param>
        /// <returns>ID da solicitação criada</returns>
        private async Task<int> CriarSolicitacaoAjudaIoT(int usuarioId, TesteDto dados)
        {
            InserirSolicitacaoAjudaRequisicao requisicao = new InserirSolicitacaoAjudaRequisicao()
            {
                IdUsuario = usuarioId,
                Descricao = $"Solicitação automática do dispositivo IoT - Coordenadas: {dados.lat}, {dados.lng}",
                Latitude = dados.lat,
                Longitude = dados.lng,
                EnderecoReferencia = "Localização detectada pelo dispositivo IoT"
            };

            InserirSolicitacaoAjudaResultado resultado = await _mediator.Send(requisicao);
            
            return resultado.SolicitacaoAjuda.IdSolicitacaoAjuda;
        }


        [HttpGet("TesteGet")]
        public string TesteGet([FromQuery]object testeDto)
        {
            return JsonSerializer.Serialize(testeDto, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
