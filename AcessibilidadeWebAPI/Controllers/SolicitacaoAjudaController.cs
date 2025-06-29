using AcessibilidadeWebAPI.Models.Assistencias;
using AcessibilidadeWebAPI.Models.Auth;
using AcessibilidadeWebAPI.Models.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Requisicoes.Assistencias;
using AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Resultados.Assistencias;
using AcessibilidadeWebAPI.Resultados.SolicitacaoAjudas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AcessibilidadeWebAPI.Controllers
{
    /// <summary>
    /// Controlador para solicitações de ajuda
    /// </summary>
    [Route("api/solicitacoes")]
    public class SolicitacaoAjudaController : ApiController
    {
        /// <summary>
        /// Criar nova solicitação de ajuda (apenas deficientes autenticados)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(InserirSolicitacaoAjudaOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InserirSolicitacaoAjudaOutput>> CriarSolicitacao([FromBody] CriarSolicitacaoRequest input, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                int userId = int.Parse(userIdClaim.Value);

                InserirSolicitacaoAjudaRequisicao requisicao = new InserirSolicitacaoAjudaRequisicao()
                {
                    IdUsuario = userId,
                    Descricao = input.Descricao,
                    Latitude = input.Latitude,
                    Longitude = input.Longitude,
                    EnderecoReferencia = input.EnderecoReferencia
                };

                InserirSolicitacaoAjudaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

                InserirSolicitacaoAjudaOutput output = new InserirSolicitacaoAjudaOutput()
                {
                    SolicitacaoAjuda = resultado.SolicitacaoAjuda,
                };

                return CreatedAtAction(nameof(ObterSolicitacao), new { id = resultado.SolicitacaoAjuda.IdSolicitacaoAjuda }, output);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao criar solicitação", error = ex.Message });
            }
        }

        /// <summary>
        /// Listar minhas solicitações de ajuda
        /// </summary>
        /// <param name="cancellationToken"></param>
        [HttpGet("minhas")]
        [Authorize]
        [ProducesResponseType(typeof(ListarSolicitacaoAjudaOutput), StatusCodes.Status200OK)]
        public async Task<ActionResult<ListarSolicitacaoAjudaOutput>> MinhasSolicitacoes(CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                int userId = int.Parse(userIdClaim.Value);

                ListarSolicitacaoAjudaRequisicao requisicao = new ListarSolicitacaoAjudaRequisicao()
                {
                    IdUsuario = userId,
                };

                ListarSolicitacaoAjudaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

                ListarSolicitacaoAjudaOutput output = new ListarSolicitacaoAjudaOutput()
                {
                    ArrSolicitacaoAjuda = resultado.ArrSolicitacaoAjuda
                };

                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Obter solicitação específica
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ObterSolicitacaoAjudaOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ObterSolicitacaoAjudaOutput>> ObterSolicitacao(int id, CancellationToken cancellationToken)
        {
            try
            {
                ObterSolicitacaoAjudaRequisicao requisicao = new ObterSolicitacaoAjudaRequisicao()
                {
                    IdSolicitacaoAjuda = id,
                };

                ObterSolicitacaoAjudaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

                if (resultado.SolicitacaoAjuda == null)
                {
                    return NotFound(new { message = "Solicitação não encontrada" });
                }

                ObterSolicitacaoAjudaOutput output = new ObterSolicitacaoAjudaOutput()
                {
                    SolicitacaoAjuda = resultado.SolicitacaoAjuda
                };

                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Listar solicitações de ajuda disponíveis (para voluntários)
        /// </summary>
        /// <param name="latitude">Latitude atual do voluntário</param>
        /// <param name="longitude">Longitude atual do voluntário</param>
        /// <param name="raioKm">Raio de busca em quilômetros (padrão: 10km)</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("disponiveis")]
        [ProducesResponseType(typeof(SolicitacoesDisponiveisOutput), StatusCodes.Status200OK)]
        public async Task<ActionResult<SolicitacoesDisponiveisOutput>> SolicitacoesDisponiveis(
            [FromQuery] double? latitude, 
            [FromQuery] double? longitude, 
            [FromQuery] double raioKm = 10.0, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Buscar todas as solicitações pendentes
                ListarSolicitacaoAjudaRequisicao requisicao = new ListarSolicitacaoAjudaRequisicao()
                {
                    IdUsuario = 0 // 0 = buscar todas
                };

                ListarSolicitacaoAjudaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

                // Filtrar apenas as pendentes e calcular distâncias
                var solicitacoesComDistancia = resultado.ArrSolicitacaoAjuda
                    .Where(s => s.Status == StatusSolicitacao.Pendente) // StatusSolicitacao.Pendente
                    .Select(s => new SolicitacaoComDistancia
                    {
                        IdSolicitacaoAjuda = s.IdSolicitacaoAjuda,
                        DeficienteUsuarioId = s.DeficienteUsuarioId,
                        Descricao = s.Descricao,
                        DataSolicitacao = s.DataSolicitacao,
                        Latitude = s.Latitude, // Será atualizado quando o DTO for corrigido
                        Longitude = s.Longitude, // Será atualizado quando o DTO for corrigido
                        EnderecoReferencia = s.EnderecoReferencia, // Será atualizado quando o DTO for corrigido
                        DistanciaKm = CalcularDistancia(latitude, longitude, s.Latitude, s.Longitude)
                    })
                    .Where(s => s.DistanciaKm <= raioKm || s.DistanciaKm == -1) // -1 = sem localização
                    .OrderBy(s => s.DistanciaKm == -1 ? double.MaxValue : s.DistanciaKm)
                    .ToArray();

                SolicitacoesDisponiveisOutput output = new SolicitacoesDisponiveisOutput()
                {
                    Solicitacoes = solicitacoesComDistancia
                };

                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Ver quem respondeu à minha solicitação de ajuda
        /// </summary>
        /// <param name="id">ID da solicitação</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{id}/assistencias")]
        [Authorize]
        [ProducesResponseType(typeof(ListarAssistenciaOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ListarAssistenciaOutput>> VerRespostas(int id, CancellationToken cancellationToken)
        {
            try
            {
                ListarAssistenciaRequisicao requisicao = new ListarAssistenciaRequisicao()
                {
                    IdSolicitacaoAjuda = id,
                    IdUsuario = 0 // 0 indica: listar todas as assistências desta solicitação
                };

                ListarAssistenciaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

                ListarAssistenciaOutput output = new ListarAssistenciaOutput()
                {
                    ArrAssistencia = resultado.ArrAssistencia
                };

                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Excluir SolicitacaoAjuda
        /// </summary>
        /// <remarks> Excluir SolicitacaoAjuda </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpDelete("api/[controller]/ExcluirSolicitacaoAjuda")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<NoContentResult> ExcluirSolicitacaoAjuda(ExcluirSolicitacaoAjudaInput input, CancellationToken cancellationToken)
        {
            ExcluirSolicitacaoAjudaRequisicao requisicao = new ExcluirSolicitacaoAjudaRequisicao()
            {
                IdSolicitacaoAjuda = input.IdSolicitacaoAjuda,
            };

            await Mediator.Send(requisicao, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Listar SolicitacaoAjuda
        /// </summary>
        /// <remarks> Listar SolicitacaoAjuda </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ListarSolicitacaoAjuda")]
        [ProducesResponseType(typeof(ListarSolicitacaoAjudaOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ListarSolicitacaoAjuda([FromQuery] ListarSolicitacaoAjudaInput input, CancellationToken cancellationToken)
        {
            ListarSolicitacaoAjudaRequisicao requisicao = new ListarSolicitacaoAjudaRequisicao()
            {
                IdUsuario = input.IdUsuario,
            };

            ListarSolicitacaoAjudaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ListarSolicitacaoAjudaOutput output = new ListarSolicitacaoAjudaOutput()
            {
                ArrSolicitacaoAjuda = resultado.ArrSolicitacaoAjuda
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Inserir SolicitacaoAjuda
        /// </summary>
        /// <remarks> Inserir SolicitacaoAjuda </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("api/[controller]/InserirSolicitacaoAjuda")]
        [ProducesResponseType(typeof(InserirSolicitacaoAjudaOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> InserirSolicitacaoAjuda(InserirSolicitacaoAjudaInput input, CancellationToken cancellationToken)
        {
            InserirSolicitacaoAjudaRequisicao requisicao = new InserirSolicitacaoAjudaRequisicao()
            {
                IdUsuario= input.IdUsuario,
                Descricao = input.Descricao,
                EnderecoReferencia = input.EnderecoReferencia,
                Latitude = input.Latitude,
                Longitude = input.Longitude
            };

            InserirSolicitacaoAjudaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            InserirSolicitacaoAjudaOutput output = new InserirSolicitacaoAjudaOutput()
            {
                SolicitacaoAjuda = resultado.SolicitacaoAjuda,
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Editar SolicitacaoAjuda
        /// </summary>
        /// <remarks> Editar SolicitacaoAjuda </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("api/[controller]/EditarSolicitacaoAjuda")]
        [ProducesResponseType(typeof(EditarSolicitacaoAjudaOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> EditarSolicitacaoAjuda(EditarSolicitacaoAjudaInput input, CancellationToken cancellationToken)
        {
            EditarSolicitacaoAjudaRequisicao requisicao = new EditarSolicitacaoAjudaRequisicao()
            {
                IdSolicitacaoAjuda = input.IdSolicitacaoAjuda,
                Descricao = input.Descricao,
                Status = input.Status,
                DataSolicitacao = input.DataSolicitacao,
                IdUsuario = input.IdUsuario,
                DataResposta = input.DataResposta
            };

            EditarSolicitacaoAjudaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            EditarSolicitacaoAjudaOutput output = new EditarSolicitacaoAjudaOutput()
            {
                IdSolicitacaoAjuda = resultado.IdSolicitacaoAjuda,
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Calcular distância entre duas coordenadas usando fórmula de Haversine
        /// </summary>
        private static double CalcularDistancia(double? lat1, double? lon1, double? lat2, double? lon2)
        {
            if (!lat1.HasValue || !lon1.HasValue || !lat2.HasValue || !lon2.HasValue)
            {
                return -1; // Indica que não há localização disponível
            }

            const double R = 6371; // Raio da Terra em km

            var dLat = ToRadians(lat2.Value - lat1.Value);
            var dLon = ToRadians(lon2.Value - lon1.Value);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1.Value)) * Math.Cos(ToRadians(lat2.Value)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        /// <summary>
        /// Converter graus para radianos
        /// </summary>
        private static double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
