using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals
{
    public class ExcluirAvaliacaoLocalRequisicao : IRequest
    {
        public int IdAvaliacaoLocal { get; set; }

    }
}
