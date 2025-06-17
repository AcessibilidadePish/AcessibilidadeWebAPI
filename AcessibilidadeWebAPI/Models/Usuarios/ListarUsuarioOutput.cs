using AcessibilidadeWebAPI.Dtos.Usuario;

namespace AcessibilidadeWebAPI.Models.Usuarios
{
    public class ListarUsuarioOutput
    {
        public IEnumerable<UsuarioDto> ArrUsuario { get; set; }
    }
}
