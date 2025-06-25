namespace AcessibilidadeWebAPI.Entidades
{
    public class AvaliacaoLocal
    {
        public int IdAvaliacaoLocal { get; set; }
        public int IdLocal { get; set; }
        public bool Acessivel {  get; set; }
        public string Observacao {  get; set; }

        public int Timestamp { get; set; }
        public virtual Local IdLocalNavigation { get; set; }
    }
}
