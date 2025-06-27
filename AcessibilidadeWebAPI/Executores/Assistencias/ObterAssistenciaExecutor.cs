using AcessibilidadeWebAPI.Dtos.Assistencia;
using AcessibilidadeWebAPI.Repositorios.Assistencias;
using AcessibilidadeWebAPI.Requisicoes.Assistencias;
using AcessibilidadeWebAPI.Resultados.Assistencias;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Assistencias
{
    public class ObterAssistenciaExecutor : IRequestHandler<ObterAssistenciaRequisicao, ObterAssistenciaResultado>
    {
        private readonly IMapper mapper;
        private readonly IAssistenciaRepositorio assistenciaRepositorio;

        public ObterAssistenciaExecutor(IMapper mapper, IAssistenciaRepositorio assistenciaRepositorio)
        {
            this.mapper = mapper;
            this.assistenciaRepositorio = assistenciaRepositorio;
        }
        public Task<ObterAssistenciaResultado> Handle(ObterAssistenciaRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.Assistencia Assistencia = assistenciaRepositorio.ObterPorId(request.IdAssistencia);

            var assistenciaDto = mapper.Map<AssistenciaDto>(Assistencia);

            return Task.FromResult(new ObterAssistenciaResultado()
            {
                Assistencia = assistenciaDto,
            });
        }
    }
}
