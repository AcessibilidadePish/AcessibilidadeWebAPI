using AcessibilidadeWebAPI.Models.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas;
using AcessibilidadeWebAPI.Resultados.SolicitacaoAjudas;
using Microsoft.AspNetCore.Mvc;

namespace AcessibilidadeWebAPI.Controllers
{
    public class SolicitacaoAjudaController : ApiController
    {/// <summary>
     /// ObterSolicitacaoAjuda
     /// </summary>
     /// <remarks> ObterSolicitacaoAjuda </remarks>
     /// <param name="IdSolicitacaoAjuda"></param>
     /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ObterSolicitacaoAjuda")]
        [ProducesResponseType(typeof(ObterSolicitacaoAjudaOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ObterSolicitacaoAjuda(int IdSolicitacaoAjuda, CancellationToken cancellationToken)
        {
            ObterSolicitacaoAjudaRequisicao requisicao = new ObterSolicitacaoAjudaRequisicao()
            {
                IdSolicitacaoAjuda = IdSolicitacaoAjuda,
            };

            ObterSolicitacaoAjudaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ObterSolicitacaoAjudaOutput output = new ObterSolicitacaoAjudaOutput()
            {
                SolicitacaoAjuda = resultado.SolicitacaoAjuda
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
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
                DataSolicitacao = input.DataSolicitacao,
                Status = input.Status,
                Descricao = input.Descricao
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
    }
}
