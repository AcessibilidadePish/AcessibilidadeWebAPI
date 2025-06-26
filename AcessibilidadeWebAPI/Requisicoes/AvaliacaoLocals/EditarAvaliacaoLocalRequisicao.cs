using AcessibilidadeWebAPI.Resultados.AvaliacaoLocals;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals
{
    public class EditarAvaliacaoLocalRequisicao : IRequest<EditarAvaliacaoLocalResultado>
    {
        public int IdAvaliacaoLocal { get; set; }
        public int IdLocal { get; set; }
        public bool Acessivel { get; set; }
        public string Observacao { get; set; }

        public int Timestamp { get; set; }
    }
}
