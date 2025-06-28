namespace AcessibilidadeWebAPI.Models.Voluntarios
{
    public class EstatisticasPorRegiaoOutput
    {
        public IEnumerable<EstatisticaRegiao> Estatisticas { get; set; } = new List<EstatisticaRegiao>();
    }

    public class EstatisticaRegiao
    {
        public string Regiao { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PercentualDisponivel { get; set; }
        public decimal AvaliacaoMedia { get; set; }
    }
} 