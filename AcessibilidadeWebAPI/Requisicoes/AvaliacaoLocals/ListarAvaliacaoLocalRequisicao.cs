using AcessibilidadeWebAPI.Resultados.AvaliacaoLocals;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals
{
    public class ListarAvaliacaoLocalRequisicao : IRequest<ListarAvaliacaoLocalResultado>
    {
        public int IdLocal { get; set; }
    }
}
