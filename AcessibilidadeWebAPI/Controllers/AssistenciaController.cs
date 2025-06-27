
using AcessibilidadeWebAPI.Models.Assistencias;
using AcessibilidadeWebAPI.Requisicoes.Assistencias;
using AcessibilidadeWebAPI.Resultados.Assistencias;
using Microsoft.AspNetCore.Mvc;

namespace AcessibilidadeWebAPI.Controllers
{
    public class AssistenciaController : ApiController
    {/// <summary>
     /// ObterAssistencia
     /// </summary>
     /// <remarks> ObterAssistencia </remarks>
     /// <param name="IdAssistencia"></param>
     /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ObterAssistencia")]
        [ProducesResponseType(typeof(ObterAssistenciaOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ObterAssistencia(int IdAssistencia, CancellationToken cancellationToken)
        {
            ObterAssistenciaRequisicao requisicao = new ObterAssistenciaRequisicao()
            {
                IdAssistencia = IdAssistencia,
            };

            ObterAssistenciaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ObterAssistenciaOutput output = new ObterAssistenciaOutput()
            {
                Assistencia = resultado.Assistencia
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
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
