using AcessibilidadeWebAPI.Resultados.Usuarios;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Usuarios
{
    public class ObterUsuarioRequisicao : IRequest<ObterUsuarioResultado>
    {
        public int IdUsuario { get; set; }

    }
}
