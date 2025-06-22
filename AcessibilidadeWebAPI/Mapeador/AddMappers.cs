using AcessibilidadeWebAPI.Dtos.Deficiente;
using AcessibilidadeWebAPI.Dtos.Usuario;
using AcessibilidadeWebAPI.Dtos.Voluntario;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Requisicoes.Usuarios;
using AcessibilidadeWebAPI.Requisicoes.Voluntario;
using AutoMapper;

namespace AcessibilidadeWebAPI.Mapeador
{
    public class AddMappers : Profile
    {
        public AddMappers()
        {
            AddUsuario();
            AddVoluntario();
            AddDeficiente();
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
            CreateMap<InserirVoluntarioRequisicao, Voluntario>();
            CreateMap<EditarVoluntarioRequisicao, Voluntario>();
        }

        private void AddDeficiente()
        {
            CreateMap<Deficiente, DeficienteDto>();
        }
    }
}
