using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Usuarios
{
    public class ExcluirUsuarioRequisicao : IRequest
    {
        public int IdUsuario { get; set; }
    }
}
