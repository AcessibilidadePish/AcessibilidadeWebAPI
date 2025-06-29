namespace AcessibilidadeWebAPI.Entidades
{
    public partial class Assistencia
    {
        public int IdAssistencia { get; set; }
        public int SolicitacaoAjudaId { get; set; }
        public int VoluntarioUsuarioId { get; set; }
        public DateTimeOffset DataAceite { get; set; }
        public DateTimeOffset? DataConclusao { get; set; }
        public int? DeficienteIdUsuario { get; set; }
        public virtual SolicitacaoAjuda SolicitacaoAjudaNavigation { get; set; }
        public virtual Voluntario VoluntarioUsuarioNavigation { get; set; }
        public virtual Deficiente DeficienteUsuarioNavigation { get; set; }
    }
}
