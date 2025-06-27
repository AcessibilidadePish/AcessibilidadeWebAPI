using AcessibilidadeWebAPI.Dtos.Assistencia;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Repositorios.Assistencias;
using AcessibilidadeWebAPI.Repositorios.Assistencias;
using AcessibilidadeWebAPI.Requisicoes.Assistencias;
using AcessibilidadeWebAPI.Resultados.Assistencias;
using AcessibilidadeWebAPI.Resultados.Assistencias;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Assistencias
{
    public class InserirAssistenciaExecutor : IRequestHandler<InserirAssistenciaRequisicao, InserirAssistenciaResultado>
    {
        private readonly IMapper mapper;
        private readonly IAssistenciaRepositorio assistenciaRepositorio;

        public InserirAssistenciaExecutor(IMapper mapper, IAssistenciaRepositorio assistenciaRepositorio)
        {
            this.mapper = mapper;
            this.assistenciaRepositorio = assistenciaRepositorio;
        }
        public Task<InserirAssistenciaResultado> Handle(InserirAssistenciaRequisicao request, CancellationToken cancellationToken)
        {
            Assistencia assistencia = mapper.Map<Assistencia>(request);
            Assistencia res = assistenciaRepositorio.Inserir(assistencia);

            return Task.FromResult(new InserirAssistenciaResultado()
            {
                Assistencia = mapper.Map<AssistenciaDto>(res),
            });
        }
    }
}
