namespace AcessibilidadeWebAPI.Entidades
{
    public partial class Assistencia
    {
        public int IdAssistencia { get; set; }
        public int IdSolicitacaoAjuda { get; set; }
        public int IdUsuario { get; set; }
        public DateTimeOffset DataAceite {  get; set; }
        public DateTimeOffset DataConclusao { get; set; }
        public virtual SolicitacaoAjuda IdSolicitacaoAjudaNavigation { get; set; }
        public virtual Deficiente IdUsuarioNavigation { get; set; }
    }
}
