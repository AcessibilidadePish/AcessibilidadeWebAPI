using AcessibilidadeWebAPI.Resultados.AvaliacaoLocals;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals
{
    public class InserirAvaliacaoLocalRequisicao : IRequest<InserirAvaliacaoLocalResultado>
    {
        public int IdLocal { get; set; }
        public bool Acessivel { get; set; }
        public string Observacao { get; set; }
        public int IdUsuario { get; set; }
    }
}
