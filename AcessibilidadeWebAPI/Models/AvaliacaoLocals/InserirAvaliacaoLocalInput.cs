namespace AcessibilidadeWebAPI.Models.AvaliacaoLocals
{
    public class InserirAvaliacaoLocalInput
    {
        public int IdLocal { get; set; }
        public bool Acessivel { get; set; }
        public string Observacao { get; set; }
    }
}