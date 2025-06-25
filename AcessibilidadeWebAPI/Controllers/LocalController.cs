using AcessibilidadeWebAPI.Models.Locals;
using AcessibilidadeWebAPI.Requisicoes.Locals;
using AcessibilidadeWebAPI.Resultados.Locals;
using Microsoft.AspNetCore.Mvc;

namespace AcessibilidadeWebAPI.Controllers
{
    public class LocalController : ApiController
    {
        /// <summary>
        /// ObterLocal
        /// </summary>
        /// <remarks> ObterLocal </remarks>
        /// <param name="IdLocal"></param>
        /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ObterLocal")]
        [ProducesResponseType(typeof(ObterLocalOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ObterLocal(int IdLocal, CancellationToken cancellationToken)
        {
            ObterLocalRequisicao requisicao = new ObterLocalRequisicao()
            {
                IdLocal = IdLocal,
            };

            ObterLocalResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ObterLocalOutput output = new ObterLocalOutput()
            {
                Local = resultado.Local
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Excluir Local
        /// </summary>
        /// <remarks> Excluir Local </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpDelete("api/[controller]/ExcluirLocal")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<NoContentResult> ExcluirLocal(ExcluirLocalInput input, CancellationToken cancellationToken)
        {
            ExcluirLocalRequisicao requisicao = new ExcluirLocalRequisicao()
            {
                IdLocal = input.IdLocal,
            };

            await Mediator.Send(requisicao, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Listar Local
        /// </summary>
        /// <remarks> Listar Local </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ListarLocal")]
        [ProducesResponseType(typeof(ListarLocalOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ListarLocal([FromQuery] ListarLocalInput input, CancellationToken cancellationToken)
        {
            ListarLocalRequisicao requisicao = new ListarLocalRequisicao()
            {
            };

            ListarLocalResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ListarLocalOutput output = new ListarLocalOutput()
            {
                ArrLocal = resultado.ArrLocal
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Inserir Local
        /// </summary>
        /// <remarks> Inserir Local </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("api/[controller]/InserirLocal")]
        [ProducesResponseType(typeof(InserirLocalOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> InserirLocal(InserirLocalInput input, CancellationToken cancellationToken)
        {
            InserirLocalRequisicao requisicao = new InserirLocalRequisicao()
            {

                AvaliacaoAcessibilidade = input.AvaliacaoAcessibilidade,
                Descricao = input.Descricao,
                Latitude = input.Latitude,
                Longitude = input.Longitude
            };

            InserirLocalResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            InserirLocalOutput output = new InserirLocalOutput()
            {
                Local = resultado.Local,
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Editar Local
        /// </summary>
        /// <remarks> Editar Local </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("api/[controller]/EditarLocal")]
        [ProducesResponseType(typeof(EditarLocalOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> EditarLocal(EditarLocalInput input, CancellationToken cancellationToken)
        {
            EditarLocalRequisicao requisicao = new EditarLocalRequisicao()
            {
                IdLocal = input.IdLocal,
                Longitude = input.Longitude,
                Latitude = input.Latitude,
                AvaliacaoAcessibilidade = input.AvaliacaoAcessibilidade,
                Descricao = input.Descricao 
            };

            EditarLocalResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            EditarLocalOutput output = new EditarLocalOutput()
            {
                IdLocal = resultado.IdLocal,
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
