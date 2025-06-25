using AcessibilidadeWebAPI.Resultados.Locals;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Locals
{
    public class InserirLocalRequisicao : IRequest<InserirLocalResultado>
    {
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public string Descricao { get; set; }
        public int AvaliacaoAcessibilidade { get; set; }
    }
}
