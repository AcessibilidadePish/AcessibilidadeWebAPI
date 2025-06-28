using AcessibilidadeWebAPI.Dtos.AvaliacaoLocal;

namespace AcessibilidadeWebAPI.Models.AvaliacaoLocals
{
    public class ListarAvaliacaoCompletaOutput
    {
        public IEnumerable<AvaliacaoLocalCompletaDto> AvaliacoesCompletas { get; set; } = new List<AvaliacaoLocalCompletaDto>();
        public int Total { get; set; }
        public int PaginaAtual { get; set; }
        public int TamanhoPagina { get; set; }
        public bool TemProximaPagina { get; set; }
    }
} 