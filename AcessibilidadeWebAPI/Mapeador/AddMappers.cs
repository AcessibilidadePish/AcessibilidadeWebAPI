using AcessibilidadeWebAPI.Dtos.Assistencia;
using AcessibilidadeWebAPI.Dtos.AvaliacaoLocal;
using AcessibilidadeWebAPI.Dtos.Deficiente;
using AcessibilidadeWebAPI.Dtos.Local;
using AcessibilidadeWebAPI.Dtos.SolicitacaoAjuda;
using AcessibilidadeWebAPI.Dtos.Usuario;
using AcessibilidadeWebAPI.Dtos.Voluntario;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Models.Auth;
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
            AddAuth();
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
            CreateMap<InserirDeficienteRequisicao, Deficiente>();
            CreateMap<EditarDeficienteRequisicao, Deficiente>();
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

        private void AddAuth()
        {
            CreateMap<Usuario, UsuarioInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdUsuario))
                .ForMember(dest => dest.TipoUsuario, opt => opt.Ignore())
                .ForMember(dest => dest.Voluntario, opt => opt.Ignore())
                .ForMember(dest => dest.Deficiente, opt => opt.Ignore());

            CreateMap<Voluntario, VoluntarioInfo>();

            CreateMap<Deficiente, DeficienteInfo>()
                .ForMember(dest => dest.TipoDeficiencia, opt => opt.MapFrom(src => (int)src.TipoDeficiencia))
                .ForMember(dest => dest.TipoDeficienciaDescricao, opt => opt.MapFrom(src => GetTipoDeficienciaDescricao((int)src.TipoDeficiencia)));
        }

        private string GetTipoDeficienciaDescricao(int tipoDeficiencia)
        {
            return tipoDeficiencia switch
            {
                1 => "Física",
                2 => "Visual",
                3 => "Auditiva",
                4 => "Cognitiva",
                5 => "Múltipla",
                6 => "Outra",
                _ => "Não especificado"
            };
        }
    }
}
