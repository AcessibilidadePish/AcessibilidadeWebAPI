using AcessibilidadeWebAPI.Resultados.Locals;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Locals
{
    public class InserirLocalRequisicao : IRequest<InserirLocalResultado>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Descricao { get; set; }
        public int AvaliacaoAcessibilidade { get; set; }
    }
}
