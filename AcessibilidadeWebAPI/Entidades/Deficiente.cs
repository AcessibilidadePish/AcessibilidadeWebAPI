namespace AcessibilidadeWebAPI.Entidades
{
    public partial class Deficiente
    {
        public int IdUsuario { get; set; }
        public int TipoDeficiencia { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
