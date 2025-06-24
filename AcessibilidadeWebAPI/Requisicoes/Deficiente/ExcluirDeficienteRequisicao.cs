using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Deficiente
{
    public class ExcluirDeficienteRequisicao : IRequest
    {
        public int IdUsuario { get; set; }

    }
}
