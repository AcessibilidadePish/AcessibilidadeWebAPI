namespace AcessibilidadeWebAPI.Entidades
{
    public partial class Voluntario
    {
        public Voluntario()
        {
            Assistencias = new HashSet<Assistencia>();
        }

        public int IdUsuario { get; set; }
        public bool Disponivel { get; set; }
        public decimal Avaliacao { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Assistencia> Assistencias { get; set; }
    }
}
