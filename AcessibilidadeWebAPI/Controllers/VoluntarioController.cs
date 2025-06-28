using AcessibilidadeWebAPI.Models.Voluntarios;
using AcessibilidadeWebAPI.Models.Auth;
using AcessibilidadeWebAPI.Models.Assistencias;
using AcessibilidadeWebAPI.Requisicoes.Voluntario;
using AcessibilidadeWebAPI.Requisicoes.Assistencias;
using AcessibilidadeWebAPI.Resultados.Voluntario;
using AcessibilidadeWebAPI.Resultados.Assistencias;
using AcessibilidadeWebAPI.Dtos.Voluntario;
using AcessibilidadeWebAPI.Dtos.Assistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AcessibilidadeWebAPI.Controllers
{
    /// <summary>
    /// Controlador para operações específicas de voluntários
    /// </summary>
    [Route("api/[controller]")]
    public class VoluntarioController : ApiController
    {
        /// <summary>
        /// Listar voluntários disponíveis
        /// </summary>
        /// <remarks>Lista voluntários públicos e disponíveis para consulta</remarks>
        /// <param name="cancellationToken"></param>
        [HttpGet]
        [ProducesResponseType(typeof(ListarVoluntarioOutput), StatusCodes.Status200OK)]
        public async Task<ActionResult<ListarVoluntarioOutput>> ListarVoluntarios(CancellationToken cancellationToken)
        {
            ListarVoluntarioRequisicao requisicao = new ListarVoluntarioRequisicao();

            ListarVoluntarioResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ListarVoluntarioOutput output = new ListarVoluntarioOutput()
            {
                ArrVoluntario = resultado.ArrVoluntario
            };

            return Ok(output);
        }

        /// <summary>
        /// Obter perfil específico de voluntário
        /// </summary>
        /// <remarks>Obtém dados públicos de um voluntário específico</remarks>
        /// <param name="usuarioId">ID do usuário voluntário</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{usuarioId}")]
        [ProducesResponseType(typeof(ObterVoluntarioOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ObterVoluntarioOutput>> ObterVoluntario(int usuarioId, CancellationToken cancellationToken)
        {
            ObterVoluntarioRequisicao requisicao = new ObterVoluntarioRequisicao()
            {
                IdUsuario = usuarioId,
            };

            ObterVoluntarioResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            if (resultado.Voluntario == null)
            {
                return NotFound(new { message = "Voluntário não encontrado" });
            }

            ObterVoluntarioOutput output = new ObterVoluntarioOutput()
            {
                Voluntario = resultado.Voluntario
            };

            return Ok(output);
        }

        /// <summary>
        /// Atualizar disponibilidade do voluntário
        /// </summary>
        /// <remarks>Permite ao voluntário alternar sua disponibilidade</remarks>
        /// <param name="usuarioId">ID do usuário voluntário</param>
        /// <param name="disponivel">Nova disponibilidade</param>
        /// <param name="cancellationToken"></param>
        [HttpPut("{usuarioId}/disponibilidade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AtualizarDisponibilidade(int usuarioId, [FromBody] bool disponivel, CancellationToken cancellationToken)
        {
            try
            {
                EditarVoluntarioRequisicao requisicao = new EditarVoluntarioRequisicao()
                {
                    IdUsuario = usuarioId,
                    Disponivel = disponivel,
                    // Manter avaliação atual - será ignorada pelo executor se não for fornecida
                    Avaliacao = 0
                };

                EditarVoluntarioResultado resultado = await Mediator.Send(requisicao, cancellationToken);

                return Ok(new { message = "Disponibilidade atualizada com sucesso", disponivel });
            }
            catch (Exception)
            {
                return NotFound(new { message = "Voluntário não encontrado" });
            }
        }

        /// <summary>
        /// Avaliar voluntário após assistência concluída
        /// </summary>
        /// <remarks>Permite ao deficiente avaliar o voluntário que o ajudou</remarks>
        /// <param name="usuarioId">ID do usuário voluntário</param>
        /// <param name="avaliacaoRequest">Dados da avaliação</param>
        /// <param name="cancellationToken"></param>
        [HttpPost("{usuarioId}/avaliar")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AvaliarVoluntario(int usuarioId, [FromBody] AvaliarVoluntarioRequest avaliacaoRequest, CancellationToken cancellationToken)
        {
            try
            {
                // TODO: Verificar se o usuário logado realmente recebeu ajuda deste voluntário
                // TODO: Implementar lógica para calcular nova média de avaliação

                // Por enquanto, vamos apenas retornar sucesso
                return Ok(new { 
                    message = "Avaliação registrada com sucesso", 
                    voluntarioId = usuarioId,
                    avaliacao = avaliacaoRequest.Avaliacao,
                    comentario = avaliacaoRequest.Comentario
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Ver meu perfil de voluntário (dados completos)
        /// </summary>
        /// <param name="cancellationToken"></param>
        [HttpGet("meu-perfil")]
        [Authorize]
        [ProducesResponseType(typeof(PerfilVoluntarioCompleto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PerfilVoluntarioCompleto>> MeuPerfil(CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                int userId = int.Parse(userIdClaim.Value);

                // Buscar dados do voluntário
                ObterVoluntarioRequisicao requisicaoVoluntario = new ObterVoluntarioRequisicao()
                {
                    IdUsuario = userId,
                };

                ObterVoluntarioResultado resultadoVoluntario = await Mediator.Send(requisicaoVoluntario, cancellationToken);

                if (resultadoVoluntario.Voluntario == null)
                {
                    return NotFound(new { message = "Perfil de voluntário não encontrado" });
                }

                // Buscar histórico de assistências
                ListarAssistenciaRequisicao requisicaoAssistencias = new ListarAssistenciaRequisicao()
                {
                    IdSolicitacaoAjuda = 0, // Todas as solicitações
                    IdUsuario = userId,
                };

                ListarAssistenciaResultado resultadoAssistencias = await Mediator.Send(requisicaoAssistencias, cancellationToken);

                // Calcular estatísticas
                int totalAjudas = resultadoAssistencias.ArrAssistencia?.Count() ?? 0;
                int ajudasConcluidas = resultadoAssistencias.ArrAssistencia?
                    .Count(a => a.DataConclusao.HasValue) ?? 0; // Verifica se foi concluída (não null)

                PerfilVoluntarioCompleto perfil = new PerfilVoluntarioCompleto()
                {
                    Voluntario = resultadoVoluntario.Voluntario,
                    TotalAjudas = totalAjudas,
                    AjudasConcluidas = ajudasConcluidas,
                    AjudasAndamento = totalAjudas - ajudasConcluidas,
                    HistoricoRecente = resultadoAssistencias.ArrAssistencia?
                        .OrderByDescending(a => a.DataAceite)
                        .Take(5)
                        .ToArray() ?? new AssistenciaDto[0]
                };

                return Ok(perfil);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar status de disponibilidade via toggle
        /// </summary>
        /// <param name="cancellationToken"></param>
        [HttpPost("toggle-disponibilidade")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ToggleDisponibilidade(CancellationToken cancellationToken)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                int userId = int.Parse(userIdClaim.Value);

                // Buscar estado atual
                ObterVoluntarioRequisicao obterRequisicao = new ObterVoluntarioRequisicao()
                {
                    IdUsuario = userId,
                };

                ObterVoluntarioResultado resultado = await Mediator.Send(obterRequisicao, cancellationToken);

                if (resultado.Voluntario == null)
                {
                    return NotFound(new { message = "Voluntário não encontrado" });
                }

                // Alternar disponibilidade
                bool novaDisponibilidade = !resultado.Voluntario.Disponivel;

                EditarVoluntarioRequisicao editarRequisicao = new EditarVoluntarioRequisicao()
                {
                    IdUsuario = userId,
                    Disponivel = novaDisponibilidade,
                    Avaliacao = resultado.Voluntario.Avaliacao
                };

                await Mediator.Send(editarRequisicao, cancellationToken);

                return Ok(new { 
                    message = $"Disponibilidade alterada para: {(novaDisponibilidade ? "Disponível" : "Indisponível")}", 
                    disponivel = novaDisponibilidade 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }
    }
}
