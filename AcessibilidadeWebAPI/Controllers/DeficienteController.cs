using AcessibilidadeWebAPI.Models.Deficiente;
using AcessibilidadeWebAPI.Requisicoes.Deficiente;
using AcessibilidadeWebAPI.Resultados.Deficiente;
using Microsoft.AspNetCore.Mvc;

namespace AcessibilidadeWebAPI.Controllers
{
    public class DeficienteController : ApiController
    {
        /// <summary>
        /// ObterDeficiente
        /// </summary>
        /// <remarks> ObterDeficiente </remarks>
        /// <param name="IdDeficiente"></param>
        /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ObterDeficiente")]
        [ProducesResponseType(typeof(ObterDeficienteOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ObterDeficiente(int IdUsuario, CancellationToken cancellationToken)
        {
            ObterDeficienteRequisicao requisicao = new ObterDeficienteRequisicao()
            {
                IdUsuario = IdUsuario,
            };

            ObterDeficienteResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ObterDeficienteOutput output = new ObterDeficienteOutput()
            {
                Deficiente = resultado.Deficiente
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Excluir Deficiente
        /// </summary>
        /// <remarks> Excluir Deficiente </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpDelete("api/[controller]/ExcluirDeficiente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<NoContentResult> ExcluirDeficiente(ExcluirDeficienteInput input, CancellationToken cancellationToken)
        {
            ExcluirDeficienteRequisicao requisicao = new ExcluirDeficienteRequisicao()
            {
                IdUsuario = input.IdUsuario,
            };

            await Mediator.Send(requisicao, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Listar Deficiente
        /// </summary>
        /// <remarks> Listar Deficiente </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ListarDeficiente")]
        [ProducesResponseType(typeof(ListarDeficienteOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ListarDeficiente([FromQuery] ListarDeficienteInput input, CancellationToken cancellationToken)
        {
            ListarDeficienteRequisicao requisicao = new ListarDeficienteRequisicao()
            {
            };

            ListarDeficienteResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ListarDeficienteOutput output = new ListarDeficienteOutput()
            {
                ArrDeficiente = resultado.ArrDeficiente
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Inserir Deficiente
        /// </summary>
        /// <remarks> Inserir Deficiente </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("api/[controller]/InserirDeficiente")]
        [ProducesResponseType(typeof(InserirDeficienteOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> InserirDeficiente(InserirDeficienteInput input, CancellationToken cancellationToken)
        {
            InserirDeficienteRequisicao requisicao = new InserirDeficienteRequisicao()
            {
                
                IdUsuario = input.IdUsuario,
            };

            InserirDeficienteResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            InserirDeficienteOutput output = new InserirDeficienteOutput()
            {
                Deficiente = resultado.Deficiente,
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Editar Deficiente
        /// </summary>
        /// <remarks> Editar Deficiente </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("api/[controller]/EditarDeficiente")]
        [ProducesResponseType(typeof(EditarDeficienteOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> EditarDeficiente(EditarDeficienteInput input, CancellationToken cancellationToken)
        {
            EditarDeficienteRequisicao requisicao = new EditarDeficienteRequisicao()
            {
                IdUsuario = input.IdUsuario,
                TipoDeficiencia = input.TipoDeficiencia,
            };

            EditarDeficienteResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            EditarDeficienteOutput output = new EditarDeficienteOutput()
            {
                IdUsuario = resultado.IdUsuario,
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
