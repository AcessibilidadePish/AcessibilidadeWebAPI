namespace AcessibilidadeWebAPI.Models.AvaliacaoLocals
{
    public class EditarAvaliacaoLocalInput
    {
        public int IdAvaliacaoLocal { get; set; }
        public int IdLocal { get; set; }
        public bool Acessivel { get; set; }
        public string Observacao { get; set; }

        public int Timestamp { get; set; }
    }
}
