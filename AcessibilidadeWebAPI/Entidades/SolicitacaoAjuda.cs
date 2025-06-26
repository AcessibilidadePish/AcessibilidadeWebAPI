namespace AcessibilidadeWebAPI.Entidades
{
    public partial class SolicitacaoAjuda
    {
        public int IdSolicitacaoAjuda { get; set; }
        public int IdUsuario { get; set; }
        public string Descricao { get; set; }
        public int Status { get; set; }
        public DateTimeOffset DataSolicitacao { get; set; }
        public DateTimeOffset DataResposta { get; set; }
        public virtual Deficiente IdUsuarioNavigation { get; set; }
    }
}
