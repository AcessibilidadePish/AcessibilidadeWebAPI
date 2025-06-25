using AcessibilidadeWebAPI.Resultados.Locals;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Locals
{
    public class EditarLocalRequisicao : IRequest<EditarLocalResultado>
    {
        public int IdLocal { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public string Descricao { get; set; }
        public int AvaliacaoAcessibilidade { get; set; }
    }
}
