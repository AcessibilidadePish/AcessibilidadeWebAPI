using AcessibilidadeWebAPI.Models.Usuarios;
using AcessibilidadeWebAPI.Models.Voluntarios;
using AcessibilidadeWebAPI.Requisicoes.Usuarios;
using AcessibilidadeWebAPI.Requisicoes.Voluntario;
using AcessibilidadeWebAPI.Resultados.Usuarios;
using AcessibilidadeWebAPI.Resultados.Voluntario;
using Microsoft.AspNetCore.Mvc;

namespace AcessibilidadeWebAPI.Controllers
{
    public class VoluntarioController : ApiController
    {
        /// <summary>
        /// ObterVoluntario
        /// </summary>
        /// <remarks> ObterVoluntario </remarks>
        /// <param name="IdUsuario"></param>
        /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ObterVoluntario")]
        [ProducesResponseType(typeof(ObterVoluntarioOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ObterVoluntario(int IdUsuario, CancellationToken cancellationToken)
        {
            ObterVoluntarioRequisicao requisicao = new ObterVoluntarioRequisicao()
            {
                IdUsuario = IdUsuario
            };

            ObterVoluntarioResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ObterVoluntarioOutput output = new ObterVoluntarioOutput()
            {
                Voluntario = resultado.Voluntario
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Excluir Voluntario
        /// </summary>
        /// <remarks> Excluir Voluntario </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpDelete("api/[controller]/ExcluirVoluntario")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<NoContentResult> ExcluirVoluntario(ExcluirVoluntarioInput input, CancellationToken cancellationToken)
        {
            ExcluirVoluntarioRequisicao requisicao = new ExcluirVoluntarioRequisicao()
            {
                IdUsuario = input.IdUsuario,
            };

            await Mediator.Send(requisicao, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Listar Voluntario
        /// </summary>
        /// <remarks> Listar Voluntario </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ListarVoluntario")]
        [ProducesResponseType(typeof(ListarVoluntarioOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ListarVoluntario([FromQuery] ListarVoluntarioInput input, CancellationToken cancellationToken)
        {
            ListarVoluntarioRequisicao requisicao = new ListarVoluntarioRequisicao()
            {
            };

            ListarVoluntarioResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ListarVoluntarioOutput output = new ListarVoluntarioOutput()
            {
                ArrVoluntario = resultado.ArrVoluntario
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
