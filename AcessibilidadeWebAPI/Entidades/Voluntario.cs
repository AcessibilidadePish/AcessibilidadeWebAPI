namespace AcessibilidadeWebAPI.Entidades
{
    public class Voluntario
    {
        public int IdUsuario { get; set; }
        public bool Disponivel { get; set; }
        public int Avaliacao { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
