using AcessibilidadeWebAPI.Dtos.Usuario;
using AcessibilidadeWebAPI.Entidades;
using AutoMapper;

namespace AcessibilidadeWebAPI.Mapeador
{
    public class AddMappers : Profile
    {
        public AddMappers()
        {
            AddUsuario();
        }

        private void AddUsuario()
        {
            CreateMap<Usuario, UsuarioDto>();
        }
    }
}
