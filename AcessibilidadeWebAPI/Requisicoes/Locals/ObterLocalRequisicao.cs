using AcessibilidadeWebAPI.Resultados.Locals;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Locals
{
    public class ObterLocalRequisicao : IRequest<ObterLocalResultado>
    {
        public int IdLocal {  get; set; }
    }
}
