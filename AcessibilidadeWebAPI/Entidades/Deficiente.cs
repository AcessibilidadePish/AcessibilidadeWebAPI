using AcessibilidadeWebAPI.Models.Auth;

namespace AcessibilidadeWebAPI.Entidades
{
    public partial class Deficiente
    {
        public Deficiente()
        {
            SolicitacaoAjudas = new HashSet<SolicitacaoAjuda>();
            Assistencias = new HashSet<Assistencia>();
        }
        public int IdUsuario { get; set; }
        public TipoDeficiencia TipoDeficiencia { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<SolicitacaoAjuda> SolicitacaoAjudas { get; set; }
        public virtual ICollection<Assistencia> Assistencias { get; set; }

    }
}
