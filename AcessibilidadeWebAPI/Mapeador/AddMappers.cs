using AcessibilidadeWebAPI.Dtos.Assistencia;
using AcessibilidadeWebAPI.Dtos.AvaliacaoLocal;
using AcessibilidadeWebAPI.Dtos.Deficiente;
using AcessibilidadeWebAPI.Dtos.Local;
using AcessibilidadeWebAPI.Dtos.SolicitacaoAjuda;
using AcessibilidadeWebAPI.Dtos.Usuario;
using AcessibilidadeWebAPI.Dtos.Voluntario;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Migrations;
using AcessibilidadeWebAPI.Requisicoes.Assistencias;
using AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals;
using AcessibilidadeWebAPI.Requisicoes.Deficiente;
using AcessibilidadeWebAPI.Requisicoes.Locals;
using AcessibilidadeWebAPI.Requisicoes.SolicitacaoAjudas;
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
            AddLocal();
            AddAvaliacaoLocal();
            AddSolicitacaoAjuda();
            AddAssistencia();

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
            CreateMap<InserirDeficienteRequisicao, Voluntario>();
            CreateMap<EditarDeficienteRequisicao, Voluntario>();
        }

        private void AddLocal()
        {
            CreateMap<Local, LocalDto>();
            CreateMap<InserirLocalRequisicao, Local>();
            CreateMap<EditarLocalRequisicao, Local>();
        }

        private void AddAvaliacaoLocal()
        {
            CreateMap<AvaliacaoLocal, AvaliacaoLocalDto>();
            CreateMap<InserirAvaliacaoLocalRequisicao, AvaliacaoLocal>();
            CreateMap<EditarAvaliacaoLocalRequisicao, AvaliacaoLocal>();
        }

        private void AddSolicitacaoAjuda()
        {
            CreateMap<SolicitacaoAjuda, SolicitacaoAjudaDto>();
            CreateMap<InserirSolicitacaoAjudaRequisicao, SolicitacaoAjuda>();
            CreateMap<EditarSolicitacaoAjudaRequisicao, SolicitacaoAjuda>();
        }

        private void AddAssistencia()
        {
            CreateMap<Assistencia, AssistenciaDto>();
            CreateMap<InserirAssistenciaRequisicao, Assistencia>();
            CreateMap<EditarAssistenciaRequisicao, Assistencia>();
        }
    }
}
