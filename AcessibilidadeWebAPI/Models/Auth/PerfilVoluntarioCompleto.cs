using AcessibilidadeWebAPI.Dtos.Voluntario;
using AcessibilidadeWebAPI.Dtos.Assistencia;

namespace AcessibilidadeWebAPI.Models.Auth
{
    public class PerfilVoluntarioCompleto
    {
        public VoluntarioDto Voluntario { get; set; }
        public int TotalAjudas { get; set; }
        public int AjudasConcluidas { get; set; }
        public int AjudasAndamento { get; set; }
        public AssistenciaDto[] HistoricoRecente { get; set; }
        public string StatusDisponibilidade => Voluntario?.Disponivel == true ? "Disponível" : "Indisponível";
        public string AvaliacaoFormatada => Voluntario?.Avaliacao.ToString("F1") ?? "Sem avaliação";
    }
} 