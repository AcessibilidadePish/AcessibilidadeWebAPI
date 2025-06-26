using AcessibilidadeWebAPI.Resultados.AvaliacaoLocals;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals
{
    public class ObterAvaliacaoLocalRequisicao : IRequest<ObterAvaliacaoLocalResultado>
    {
        public int IdAvaliacaoLocal {  get; set; }
    }
}
