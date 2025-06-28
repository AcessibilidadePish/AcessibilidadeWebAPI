namespace AcessibilidadeWebAPI.Entidades
{
    public partial class Assistencia
    {
        public int IdAssistencia { get; set; }
        public int SolicitacaoAjudaId { get; set; }
        public int VoluntarioUsuarioId { get; set; }
        public DateTimeOffset DataAceite { get; set; }
        public DateTimeOffset? DataConclusao { get; set; }
        public virtual SolicitacaoAjuda SolicitacaoAjudaNavigation { get; set; }
        public virtual Voluntario VoluntarioUsuarioNavigation { get; set; }
    }
}
