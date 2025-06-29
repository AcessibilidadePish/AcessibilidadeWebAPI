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
            CreateMap<InserirAvaliacaoLocalRequisicao, AvaliacaoLocal>()
                .ForMember(dest => dest.LocalId, destinationMember => destinationMember.MapFrom(src => src.IdLocal))
                .ForMember(dest => dest.Acessivel, destinationMember => destinationMember.MapFrom(src => src.Acessivel))
                .ForMember(dest => dest.Observacoes, destinationMember => destinationMember.MapFrom(src => src.Observacao))
                .ForMember(dest => dest.Timestamp, destinationMember => destinationMember.MapFrom(src => DateTime.Now));

            CreateMap<EditarAvaliacaoLocalRequisicao, AvaliacaoLocal>();
        }

        private void AddSolicitacaoAjuda()
        {
            CreateMap<SolicitacaoAjuda, SolicitacaoAjudaDto>();
            CreateMap<InserirSolicitacaoAjudaRequisicao, SolicitacaoAjuda>()
                .ForMember(dest => dest.Descricao, destinationMember => destinationMember.MapFrom(src => src.Descricao))
                .ForMember(dest => dest.Status, destinationMember => destinationMember.MapFrom(src => StatusSolicitacao.Pendente))
                .ForMember(dest => dest.DataSolicitacao, destinationMember => destinationMember.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Latitude, destinationMember => destinationMember.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Longitude, destinationMember => destinationMember.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.EnderecoReferencia, destinationMember => destinationMember.MapFrom(src => src.EnderecoReferencia))
                .ForMember(dest => dest.DeficienteUsuarioId, destinationMember => destinationMember.MapFrom(src => src.IdUsuario));

        }

        private void AddAssistencia()
        {
            CreateMap<Assistencia, AssistenciaDto>()
                .ForMember(dest => dest.IdAssistencia, destinationMember => destinationMember.MapFrom(src => src.IdAssistencia))
                .ForMember(dest => dest.IdSolicitacaoAjuda, destinationMember => destinationMember.MapFrom(src => src.SolicitacaoAjudaId))
                .ForMember(dest => dest.IdUsuario, destinationMember => destinationMember.MapFrom(src => src.VoluntarioUsuarioId))
                .ForMember(dest => dest.DataAceite, destinationMember => destinationMember.MapFrom(src => src.DataAceite))
                .ForMember(dest => dest.DataConclusao, destinationMember => destinationMember.MapFrom(src => src.DataConclusao));
            CreateMap<InserirAssistenciaRequisicao, Assistencia>()
                .ForMember(dest => dest.SolicitacaoAjudaId, destinationMember => destinationMember.MapFrom(src => src.IdSolicitacaoAjuda))
                .ForMember(dest => dest.VoluntarioUsuarioId, destinationMember => destinationMember.MapFrom(src => src.IdUsuario))
                .ForMember(dest => dest.DataAceite, destinationMember => destinationMember.MapFrom(src => src.DataAceite))
                .ForMember(dest => dest.DataConclusao, destinationMember => destinationMember.MapFrom(src => src.DataConclusao));
            CreateMap<EditarAssistenciaRequisicao, Assistencia>()
                .ForMember(dest => dest.IdAssistencia, destinationMember => destinationMember.MapFrom(src => src.IdAssistencia))
                .ForMember(dest => dest.SolicitacaoAjudaId, destinationMember => destinationMember.MapFrom(src => src.IdSolicitacaoAjuda))
                .ForMember(dest => dest.VoluntarioUsuarioId, destinationMember => destinationMember.MapFrom(src => src.IdUsuario))
                .ForMember(dest => dest.DataAceite, destinationMember => destinationMember.MapFrom(src => src.DataAceite))
                .ForMember(dest => dest.DataConclusao, destinationMember => destinationMember.MapFrom(src => src.DataConclusao));
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