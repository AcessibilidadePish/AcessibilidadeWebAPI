using AcessibilidadeWebAPI.Models.AvaliacaoLocals;
using AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals;
using AcessibilidadeWebAPI.Resultados.AvaliacaoLocals;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            Claim? userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return new ObjectResult(new { Error = "Usuário não autenticado." })
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                };
            }

            int userId = int.Parse(userIdClaim.Value);

            InserirAvaliacaoLocalRequisicao requisicao = new InserirAvaliacaoLocalRequisicao()
            {
                IdLocal = input.IdLocal,
                Acessivel = input.Acessivel,
                Observacao = input.Observacao,
                IdUsuario = userId,
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

        /// <summary>
        /// Listar Avaliações Completas com Informações de Usuário e Local
        /// </summary>
        /// <remarks>Retorna todas as avaliações com informações detalhadas de usuário e local</remarks>
        /// <param name="pagina">Número da página (padrão: 1)</param>
        /// <param name="tamanhoPagina">Itens por página (padrão: 50)</param>
        /// <param name="localId">Filtrar por ID do local (opcional)</param>
        /// <param name="usuarioId">Filtrar por ID do usuário (opcional)</param>
        /// <param name="acessivel">Filtrar por acessibilidade (opcional)</param>
        /// <param name="dataInicio">Filtrar por data inicial (opcional)</param>
        /// <param name="dataFim">Filtrar por data final (opcional)</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ListarAvaliacoesCompletas")]
        [ProducesResponseType(typeof(ListarAvaliacaoCompletaOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ListarAvaliacoesCompletas(
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanhoPagina = 50,
            [FromQuery] int? localId = null,
            [FromQuery] int? usuarioId = null,
            [FromQuery] bool? acessivel = null,
            [FromQuery] DateTime? dataInicio = null,
            [FromQuery] DateTime? dataFim = null,
            CancellationToken cancellationToken = default)
        {
            ListarAvaliacaoCompletaRequisicao requisicao = new ListarAvaliacaoCompletaRequisicao()
            {
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina,
                LocalId = localId,
                UsuarioId = usuarioId,
                Acessivel = acessivel,
                DataInicio = dataInicio,
                DataFim = dataFim
            };

            ListarAvaliacaoCompletaResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ListarAvaliacaoCompletaOutput output = new ListarAvaliacaoCompletaOutput()
            {
                AvaliacoesCompletas = resultado.AvaliacoesCompletas,
                Total = resultado.Total,
                PaginaAtual = resultado.PaginaAtual,
                TamanhoPagina = resultado.TamanhoPagina,
                TemProximaPagina = resultado.TemProximaPagina
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
