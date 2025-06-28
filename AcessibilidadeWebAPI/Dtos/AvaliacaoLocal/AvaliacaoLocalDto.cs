namespace AcessibilidadeWebAPI.Dtos.AvaliacaoLocal
{
    public class AvaliacaoLocalDto
    {
        public int Id { get; set; }
        public int LocalId { get; set; }
        public int DispositivoId { get; set; }
        public bool Acessivel { get; set; }
        public string Observacoes { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
