using AcessibilidadeWebAPI.Dtos.Voluntario;

namespace AcessibilidadeWebAPI.Resultados.Voluntario
{
    public class EstatisticasPorRegiaoResultado
    {
        public IEnumerable<EstatisticaRegiaoDto> Estatisticas { get; set; } = new List<EstatisticaRegiaoDto>();
    }

    public class EstatisticaRegiaoDto
    {
        public string Regiao { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PercentualDisponivel { get; set; }
        public decimal AvaliacaoMedia { get; set; }
    }
} 