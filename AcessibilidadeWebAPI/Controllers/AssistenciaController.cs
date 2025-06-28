
using AcessibilidadeWebAPI.Models.Assistencias;
using AcessibilidadeWebAPI.Models.Auth;
using AcessibilidadeWebAPI.Requisicoes.Assistencias;
using AcessibilidadeWebAPI.Resultados.Assistencias;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AcessibilidadeWebAPI.Controllers
{
    /// <summary>
    /// Controlador para assistências (voluntários aceitando solicitações)
    /// </summary>
    [Route("api/assistencias")]
    public class AssistenciaController : ApiController
    {
        /// <summary>
        /// Aceitar uma solicitação de ajuda (voluntários autenticados)
        /// </summary>
        /// <param name="solicitacaoId">ID da solicitação a ser aceita</param>
        /// <param name="cancellationToken"></param>
        [HttpPost("aceitar/{solicitacaoId}")]
        [Authorize]
        [ProducesResponseType(typeof(InserirAssistenciaOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InserirAssistenciaOutput>> AceitarSolicitacao(int solicitacaoId, CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                int voluntarioId = int.Parse(userIdClaim.Value);

                InserirAssistenciaRequisicao requisicao = new InserirAssistenciaRequisicao()
                {
                    IdUsuario = voluntarioId,
                    IdSolicitacaoAjuda = solicitacaoId,
                    DataAceite = DateTimeOffset.UtcNow,
                    DataConclusao = null // null = não concluído ainda
                };

                InserirAssistenciaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

                InserirAssistenciaOutput output = new InserirAssistenciaOutput()
                {
                    Assistencia = resultado.Assistencia,
                };

                return CreatedAtAction(nameof(ObterAssistencia), new { id = resultado.Assistencia.IdAssistencia }, output);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao aceitar solicitação", error = ex.Message });
            }
        }

        /// <summary>
        /// Minhas assistências (voluntário logado)
        /// </summary>
        /// <param name="cancellationToken"></param>
        [HttpGet("minhas")]
        [Authorize]
        [ProducesResponseType(typeof(ListarAssistenciaOutput), StatusCodes.Status200OK)]
        public async Task<ActionResult<ListarAssistenciaOutput>> MinhasAssistencias(CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                int voluntarioId = int.Parse(userIdClaim.Value);

                ListarAssistenciaRequisicao requisicao = new ListarAssistenciaRequisicao()
                {
                    IdSolicitacaoAjuda = 0, // 0 = todas as solicitações
                    IdUsuario = voluntarioId,
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
        /// Obter assistência específica
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ObterAssistenciaOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ObterAssistenciaOutput>> ObterAssistencia(int id, CancellationToken cancellationToken)
        {
            try
            {
                ObterAssistenciaRequisicao requisicao = new ObterAssistenciaRequisicao()
                {
                    IdAssistencia = id,
                };

                ObterAssistenciaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

                if (resultado.Assistencia == null)
                {
                    return NotFound(new { message = "Assistência não encontrada" });
                }

                ObterAssistenciaOutput output = new ObterAssistenciaOutput()
                {
                    Assistencia = resultado.Assistencia
                };

                return Ok(output);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Excluir Assistencia
        /// </summary>
        /// <remarks> Excluir Assistencia </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpDelete("api/[controller]/ExcluirAssistencia")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<NoContentResult> ExcluirAssistencia(ExcluirAssistenciaInput input, CancellationToken cancellationToken)
        {
            ExcluirAssistenciaRequisicao requisicao = new ExcluirAssistenciaRequisicao()
            {
                IdAssistencia = input.IdAssistencia,
            };

            await Mediator.Send(requisicao, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Listar Assistencia
        /// </summary>
        /// <remarks> Listar Assistencia </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ListarAssistencia")]
        [ProducesResponseType(typeof(ListarAssistenciaOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ListarAssistencia([FromQuery] ListarAssistenciaInput input, CancellationToken cancellationToken)
        {
            ListarAssistenciaRequisicao requisicao = new ListarAssistenciaRequisicao()
            {
                IdSolicitacaoAjuda = input.IdSolicitacaoAjuda,
                IdUsuario = input.IdUsuario,
            };

            ListarAssistenciaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ListarAssistenciaOutput output = new ListarAssistenciaOutput()
            {
                ArrAssistencia = resultado.ArrAssistencia
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Inserir Assistencia
        /// </summary>
        /// <remarks> Inserir Assistencia </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("api/[controller]/InserirAssistencia")]
        [ProducesResponseType(typeof(InserirAssistenciaOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> InserirAssistencia(InserirAssistenciaInput input, CancellationToken cancellationToken)
        {
            InserirAssistenciaRequisicao requisicao = new InserirAssistenciaRequisicao()
            {
                IdUsuario = input.IdUsuario,
                IdSolicitacaoAjuda = input.IdSolicitacaoAjuda,
                DataAceite = input.DataAceite,
                DataConclusao = input.DataConclusao
            };

            InserirAssistenciaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            InserirAssistenciaOutput output = new InserirAssistenciaOutput()
            {
                Assistencia = resultado.Assistencia,
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Editar Assistencia
        /// </summary>
        /// <remarks> Editar Assistencia </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("api/[controller]/EditarAssistencia")]
        [ProducesResponseType(typeof(EditarAssistenciaOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> EditarAssistencia(EditarAssistenciaInput input, CancellationToken cancellationToken)
        {
            EditarAssistenciaRequisicao requisicao = new EditarAssistenciaRequisicao()
            {
                IdAssistencia = input.IdAssistencia,
                DataConclusao = input.DataConclusao,
                DataAceite = input.DataAceite,
                IdSolicitacaoAjuda = input.IdSolicitacaoAjuda,
                IdUsuario = input.IdUsuario
            };

            EditarAssistenciaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            EditarAssistenciaOutput output = new EditarAssistenciaOutput()
            {
                IdAssistencia = resultado.IdAssistencia,
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
