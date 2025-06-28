using AcessibilidadeWebAPI.Dtos.AvaliacaoLocal;

namespace AcessibilidadeWebAPI.Resultados.AvaliacaoLocals
{
    public class ListarAvaliacaoCompletaResultado
    {
        public IEnumerable<AvaliacaoLocalCompletaDto> AvaliacoesCompletas { get; set; } = new List<AvaliacaoLocalCompletaDto>();
        public int Total { get; set; }
        public int PaginaAtual { get; set; }
        public int TamanhoPagina { get; set; }
        public bool TemProximaPagina { get; set; }
    }
} 