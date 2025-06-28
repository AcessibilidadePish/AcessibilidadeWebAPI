using AcessibilidadeWebAPI.Models.AvaliacaoLocals;
using AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals;
using AcessibilidadeWebAPI.Resultados.AvaliacaoLocals;
using Microsoft.AspNetCore.Mvc;

namespace AcessibilidadeWebAPI.Controllers
{
    public class AvaliacaoLocalController : ApiController
    {
        /// <summary>
        /// ObterAvaliacaoLocal
        /// </summary>
        /// <remarks> ObterAvaliacaoLocal </remarks>
        /// <param name="IdAvaliacaoLocal"></param>
        /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ObterAvaliacaoLocal")]
        [ProducesResponseType(typeof(ObterAvaliacaoLocalOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ObterAvaliacaoLocal(int IdAvaliacaoLocal, CancellationToken cancellationToken)
        {
            ObterAvaliacaoLocalRequisicao requisicao = new ObterAvaliacaoLocalRequisicao()
            {
                IdAvaliacaoLocal = IdAvaliacaoLocal,
            };

            ObterAvaliacaoLocalResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ObterAvaliacaoLocalOutput output = new ObterAvaliacaoLocalOutput()
            {
                AvaliacaoLocal = resultado.AvaliacaoLocal
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Excluir AvaliacaoLocal
        /// </summary>
        /// <remarks> Excluir AvaliacaoLocal </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpDelete("api/[controller]/ExcluirAvaliacaoLocal")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<NoContentResult> ExcluirAvaliacaoLocal(ExcluirAvaliacaoLocalInput input, CancellationToken cancellationToken)
        {
            ExcluirAvaliacaoLocalRequisicao requisicao = new ExcluirAvaliacaoLocalRequisicao()
            {
                IdAvaliacaoLocal = input.IdAvaliacaoLocal,
            };

            await Mediator.Send(requisicao, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Listar AvaliacaoLocal
        /// </summary>
        /// <remarks> Listar AvaliacaoLocal </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ListarAvaliacaoLocal")]
        [ProducesResponseType(typeof(ListarAvaliacaoLocalOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ListarAvaliacaoLocal([FromQuery] ListarAvaliacaoLocalInput input, CancellationToken cancellationToken)
        {
            ListarAvaliacaoLocalRequisicao requisicao = new ListarAvaliacaoLocalRequisicao()
            {
                IdLocal = input.IdLocal,
            };

            ListarAvaliacaoLocalResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ListarAvaliacaoLocalOutput output = new ListarAvaliacaoLocalOutput()
            {
                ArrAvaliacaoLocal = resultado.ArrAvaliacaoLocal
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Inserir AvaliacaoLocal
        /// </summary>
        /// <remarks> Inserir AvaliacaoLocal </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("api/[controller]/InserirAvaliacaoLocal")]
        [ProducesResponseType(typeof(InserirAvaliacaoLocalOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> InserirAvaliacaoLocal(InserirAvaliacaoLocalInput input, CancellationToken cancellationToken)
        {
            InserirAvaliacaoLocalRequisicao requisicao = new InserirAvaliacaoLocalRequisicao()
            {
                IdLocal = input.IdLocal,
                Acessivel = input.Acessivel,
                Observacao = input.Observacao,
                Timestamp = input.Timestamp
            };

            InserirAvaliacaoLocalResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            InserirAvaliacaoLocalOutput output = new InserirAvaliacaoLocalOutput()
            {
                AvaliacaoLocal = resultado.AvaliacaoLocal,
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Editar AvaliacaoLocal
        /// </summary>
        /// <remarks> Editar AvaliacaoLocal </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("api/[controller]/EditarAvaliacaoLocal")]
        [ProducesResponseType(typeof(EditarAvaliacaoLocalOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> EditarAvaliacaoLocal(EditarAvaliacaoLocalInput input, CancellationToken cancellationToken)
        {
            EditarAvaliacaoLocalRequisicao requisicao = new EditarAvaliacaoLocalRequisicao()
            {
                IdAvaliacaoLocal = input.IdAvaliacaoLocal,
                Timestamp = input.Timestamp,
                Observacao = input.Observacao,
                Acessivel = input.Acessivel,
                IdLocal = input.IdLocal
            };

            EditarAvaliacaoLocalResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            EditarAvaliacaoLocalOutput output = new EditarAvaliacaoLocalOutput()
            {
                IdAvaliacaoLocal = resultado.IdAvaliacaoLocal,
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
