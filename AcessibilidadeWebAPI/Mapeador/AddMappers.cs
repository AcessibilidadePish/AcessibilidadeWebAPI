using AcessibilidadeWebAPI.Dtos.Usuario;
using AcessibilidadeWebAPI.Dtos.Voluntario;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Requisicoes.Usuarios;
using AutoMapper;

namespace AcessibilidadeWebAPI.Mapeador
{
    public class AddMappers : Profile
    {
        public AddMappers()
        {
            AddUsuario();
            AddVoluntario();
        }

        private void AddUsuario()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<InserirUsuarioRequisicao, Usuario>();
            CreateMap<EditarUsuarioRequisicao, Usuario>();
        }

        private void AddVoluntario()
        {
            CreateMap<Voluntario, VoluntarioDto>();
        }
    }
}
