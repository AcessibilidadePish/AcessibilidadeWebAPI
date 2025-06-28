using AcessibilidadeWebAPI.Dtos.Usuario;
using AcessibilidadeWebAPI.Dtos.Local;

namespace AcessibilidadeWebAPI.Dtos.AvaliacaoLocal
{
    public class AvaliacaoLocalCompletaDto
    {
        public int Id { get; set; }
        public int LocalId { get; set; }
        public int DispositivoId { get; set; }
        public bool Acessivel { get; set; }
        public string Observacoes { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }

        // Informações do Local
        public LocalDto? Local { get; set; }

        // Informações do Usuário (proprietário do dispositivo)
        public UsuarioDto? Usuario { get; set; }

        // Informações do Dispositivo
        public DispositivoDto? Dispositivo { get; set; }
    }

    public class DispositivoDto
    {
        public int Id { get; set; }
        public string NumeroSerie { get; set; } = string.Empty;
        public DateTime DataRegistro { get; set; }
        public int UsuarioProprietarioId { get; set; }
    }
} 