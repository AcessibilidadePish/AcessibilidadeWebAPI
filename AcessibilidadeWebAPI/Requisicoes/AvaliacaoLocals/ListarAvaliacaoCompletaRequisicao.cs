using AcessibilidadeWebAPI.Resultados.AvaliacaoLocals;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals
{
    public class ListarAvaliacaoCompletaRequisicao : IRequest<ListarAvaliacaoCompletaResultado>
    {
        public int Pagina { get; set; } = 1;
        public int TamanhoPagina { get; set; } = 50;
        public int? LocalId { get; set; }
        public int? UsuarioId { get; set; }
        public bool? Acessivel { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
} 