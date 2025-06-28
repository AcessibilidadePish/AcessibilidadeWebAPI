using AcessibilidadeWebAPI.Models.Auth;

namespace AcessibilidadeWebAPI.Entidades
{
    public partial class HistoricoStatusSolicitacao
    {
        public int Id { get; set; }
        public int SolicitacaoAjudaId { get; set; }
        public StatusSolicitacao StatusAnterior { get; set; }
        public StatusSolicitacao StatusAtual { get; set; }
        public DateTime DataMudanca { get; set; }
        public virtual SolicitacaoAjuda SolicitacaoAjudaNavigation { get; set; }
    }
} 