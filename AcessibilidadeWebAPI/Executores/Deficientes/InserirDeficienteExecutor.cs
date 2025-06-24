using AcessibilidadeWebAPI.Dtos.Deficiente;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Repositorios.Deficientes;
using AcessibilidadeWebAPI.Requisicoes.Deficiente;
using AcessibilidadeWebAPI.Resultados.Deficiente;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Deficientes
{
    public class InserirDeficienteExecutor : IRequestHandler<InserirDeficienteRequisicao, InserirDeficienteResultado>
    {
        private readonly IDeficienteRepositorio deficienteRepositorio;
        private readonly IMapper mapper;

        public InserirDeficienteExecutor(IDeficienteRepositorio deficienteRepositorio, IMapper mapper)
        {
            this.deficienteRepositorio = deficienteRepositorio;
            this.mapper = mapper;
        }
        public Task<InserirDeficienteResultado> Handle(InserirDeficienteRequisicao request, CancellationToken cancellationToken)
        {
            Deficiente Deficiente = mapper.Map<Deficiente>(request);
            Deficiente res = deficienteRepositorio.Inserir(Deficiente);

            return Task.FromResult(new InserirDeficienteResultado()
            {
                Deficiente = mapper.Map<DeficienteDto>(res),
            });
        }
    }
}
