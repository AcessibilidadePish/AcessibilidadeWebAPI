using AcessibilidadeWebAPI.Repositorios.Assistencias;
using AcessibilidadeWebAPI.Requisicoes.Assistencias;
using AcessibilidadeWebAPI.Resultados.Assistencias;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Assistencias
{
    public class EditarAssistenciaExecutor : IRequestHandler<EditarAssistenciaRequisicao, EditarAssistenciaResultado>
    {
        private readonly IMapper mapper;
        private readonly IAssistenciaRepositorio assistenciaRepositorio;

        public EditarAssistenciaExecutor(IMapper mapper, IAssistenciaRepositorio assistenciaRepositorio)
        {
            this.mapper = mapper;
            this.assistenciaRepositorio = assistenciaRepositorio;
        }
        public Task<EditarAssistenciaResultado> Handle(EditarAssistenciaRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.Assistencia assistencia = assistenciaRepositorio.ObterPorId(request.IdAssistencia);

            mapper.Map(request, assistencia);
            assistenciaRepositorio.Editar(assistencia);

            return Task.FromResult(new EditarAssistenciaResultado()
            {
                IdAssistencia = request.IdAssistencia,
            });
        }
    }
}
