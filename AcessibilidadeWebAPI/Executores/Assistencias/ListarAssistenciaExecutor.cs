using AcessibilidadeWebAPI.Dtos.Assistencia;
using AcessibilidadeWebAPI.Repositorios.Assistencias;
using AcessibilidadeWebAPI.Repositorios.Assistencias;
using AcessibilidadeWebAPI.Requisicoes.Assistencias;
using AcessibilidadeWebAPI.Resultados.Assistencias;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Assistencias
{
    public class ListarAssistenciaExecutor : IRequestHandler<ListarAssistenciaRequisicao, ListarAssistenciaResultado>
    {
        private readonly IMapper mapper;
        private readonly IAssistenciaRepositorio assistenciaRepositorio;

        public ListarAssistenciaExecutor(IMapper mapper, IAssistenciaRepositorio assistenciaRepositorio)
        {
            this.mapper = mapper;
            this.assistenciaRepositorio = assistenciaRepositorio;
        }
        public Task<ListarAssistenciaResultado> Handle(ListarAssistenciaRequisicao request, CancellationToken cancellationToken)
        {
            IQueryable<Entidades.Assistencia> arrAssistencias = assistenciaRepositorio.Listar();

            AssistenciaDto[] arrAssistenciasDto = mapper.ProjectTo<AssistenciaDto>(arrAssistencias).ToArray();

            return Task.FromResult(new ListarAssistenciaResultado()
            {
                ArrAssistencia = arrAssistenciasDto,
            });
        }
    }
}
