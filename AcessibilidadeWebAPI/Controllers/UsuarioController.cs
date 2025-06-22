using AcessibilidadeWebAPI.Models.Usuarios;
using AcessibilidadeWebAPI.Requisicoes.Usuarios;
using AcessibilidadeWebAPI.Resultados.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace AcessibilidadeWebAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        /// <summary>
        /// Obter Usuario
        /// </summary>
        /// <remarks> Obter Usuario </remarks>
        /// <param name="IdUsuario"></param>
        /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ObterUsuario")]
        [ProducesResponseType(typeof(ObterUsuarioOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ObterUsuario(int IdUsuario, CancellationToken cancellationToken)
        {
            ObterUsuarioRequisicao requisicao = new ObterUsuarioRequisicao()
            {
                IdUsuario = IdUsuario
            };

            ObterUsuarioResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ObterUsuarioOutput output = new ObterUsuarioOutput()
            {
                Usuario = resultado.Usuario
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Excluir Usuario
        /// </summary>
        /// <remarks> Excluir Usuario </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpDelete("api/[controller]/ExcluirUsuario")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<NoContentResult> ExcluirUsuario(ExcluirUsuarioInput input, CancellationToken cancellationToken)
        {
            ExcluirUsuarioRequisicao requisicao = new ExcluirUsuarioRequisicao()
            {
                IdUsuario = input.IdUsuario,
            };

            await Mediator.Send(requisicao, cancellationToken);

            return new NoContentResult();
        }

        /// <summary>
        /// Listar Usuario
        /// </summary>
        /// <remarks> Listar Usuario </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpGet("api/[controller]/ListarUsuario")]
        [ProducesResponseType(typeof(ListarUsuarioOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> ListarUsuario([FromQuery] ListarUsuarioInput input, CancellationToken cancellationToken)
        {
            ListarUsuarioRequisicao requisicao = new ListarUsuarioRequisicao()
            {
            };

            ListarUsuarioResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            ListarUsuarioOutput output = new ListarUsuarioOutput()
            {
                ArrUsuario = resultado.ArrUsuario
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Inserir Usuario
        /// </summary>
        /// <remarks> Inserir Usuario </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("api/[controller]/InserirUsuario")]
        [ProducesResponseType(typeof(InserirUsuarioOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> InserirUsuario(InserirUsuarioInput input, CancellationToken cancellationToken)
        {
            InserirUsuarioRequisicao requisicao = new InserirUsuarioRequisicao()
            {
                Senha = input.Senha,
                Email = input.Email,
                Nome = input.Nome,
                Telefone = input.Telefone
            };

            InserirUsuarioResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            InserirUsuarioOutput output = new InserirUsuarioOutput()
            {
                Usuario = resultado.Usuario,
            };

            return new ObjectResult(output)
            {
                StatusCode = StatusCodes.Status200OK,
            };
        }

        /// <summary>
        /// Editar Usuario
        /// </summary>
        /// <remarks> Editar Usuario </remarks>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        [HttpPost("api/[controller]/EditarUsuario")]
        [ProducesResponseType(typeof(EditarUsuarioOutput), StatusCodes.Status200OK)]
        public async Task<ObjectResult> EditarUsuario(EditarUsuarioInput input, CancellationToken cancellationToken)
        {
            EditarUsuarioRequisicao requisicao = new EditarUsuarioRequisicao()
            {
                IdUsuario = input.IdUsuario,
                Senha = input.Senha,
                Email = input.Email,
                Nome = input.Nome,
                Telefone = input.Telefone
            };

            EditarUsuarioResultado resultado = await Mediator.Send(requisicao, cancellationToken);

            EditarUsuarioOutput output = new EditarUsuarioOutput()
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
