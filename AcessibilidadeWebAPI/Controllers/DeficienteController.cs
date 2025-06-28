using AcessibilidadeWebAPI.Models.Deficiente;
using AcessibilidadeWebAPI.Requisicoes.Deficiente;
using AcessibilidadeWebAPI.Resultados.Deficiente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AcessibilidadeWebAPI.Controllers
{
    /// <summary>
    /// Controlador para consulta de perfis de deficientes (apenas leitura)
    /// </summary>
    [Route("api/[controller]")]
    public class DeficienteController : ApiController
    {
        /// <summary>
        /// Obter perfil público de deficiente
        /// </summary>
        /// <remarks>Obtém dados públicos de um deficiente específico (apenas para contexto de solicitações)</remarks>
        /// <param name="usuarioId">ID do usuário deficiente</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{usuarioId}")]
        [ProducesResponseType(typeof(ObterDeficienteOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ObterDeficienteOutput>> ObterDeficiente(int usuarioId, CancellationToken cancellationToken)
        {
            ObterDeficienteRequisicao requisicao = new ObterDeficienteRequisicao()
            {
                IdUsuario = usuarioId,
            };

            ObterDeficienteResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            if (resultado.Deficiente == null)
            {
                return NotFound(new { message = "Perfil de deficiente não encontrado" });
            }

            ObterDeficienteOutput output = new ObterDeficienteOutput()
            {
                Deficiente = resultado.Deficiente
            };

            return Ok(output);
        }

        // NOTA: Operações de CREATE, UPDATE e DELETE foram removidas
        // - POST: Use /auth/register para criar perfil de deficiente
        // - PUT: Tipo de deficiência raramente muda após cadastro inicial
        // - DELETE: Use operações de conta via AuthController
        // - GET (lista): Removido por questões de privacidade
    }
}
