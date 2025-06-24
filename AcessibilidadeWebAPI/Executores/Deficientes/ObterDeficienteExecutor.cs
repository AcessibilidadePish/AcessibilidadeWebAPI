using AcessibilidadeWebAPI.Dtos.Deficiente;
using AcessibilidadeWebAPI.Repositorios.Deficientes;
using AcessibilidadeWebAPI.Repositorios.Deficientes;
using AcessibilidadeWebAPI.Requisicoes.Deficiente;
using AcessibilidadeWebAPI.Resultados.Deficiente;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Deficientes
{
    public class ObterDeficienteExecutor : IRequestHandler<ObterDeficienteRequisicao, ObterDeficienteResultado>
    {
        private readonly IMapper mapper;
        private readonly IDeficienteRepositorio deficienteRepositorio;

        public ObterDeficienteExecutor(IMapper mapper, IDeficienteRepositorio deficienteRepositorio)
        {
            this.mapper = mapper;
            this.deficienteRepositorio = deficienteRepositorio;
        }
        public Task<ObterDeficienteResultado> Handle(ObterDeficienteRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.Deficiente Deficiente = deficienteRepositorio.ObterPorId(request.IdUsuario);

            DeficienteDto DeficienteDto = mapper.Map<DeficienteDto>(Deficiente);

            return Task.FromResult(new ObterDeficienteResultado()
            {
                Deficiente = DeficienteDto,
            });
        }
    }
}
