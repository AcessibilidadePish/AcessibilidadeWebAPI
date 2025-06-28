using AcessibilidadeWebAPI.Models.Auth;

namespace AcessibilidadeWebAPI.Entidades
{
    public partial class SolicitacaoAjuda
    {
        public SolicitacaoAjuda()
        {
            Assistencias = new HashSet<Assistencia>();
            HistoricoStatusSolicitacao = new HashSet<HistoricoStatusSolicitacao>();
        }
        public int IdSolicitacaoAjuda { get; set; }
        public int DeficienteUsuarioId { get; set; }
        public string Descricao { get; set; }
        public StatusSolicitacao Status { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataResposta { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? EnderecoReferencia { get; set; }
        public virtual Deficiente DeficienteUsuarioNavigation { get; set; }
        public virtual ICollection<Assistencia> Assistencias { get; set; }
        public virtual ICollection<HistoricoStatusSolicitacao> HistoricoStatusSolicitacao { get; set; }
    }
}
