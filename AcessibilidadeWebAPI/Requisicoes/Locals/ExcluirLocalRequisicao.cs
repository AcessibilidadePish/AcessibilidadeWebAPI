using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Locals
{
    public class ExcluirLocalRequisicao : IRequest
    {
        public int IdLocal { get; set; }

    }
}
