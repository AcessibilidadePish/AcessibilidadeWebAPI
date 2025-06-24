using AcessibilidadeWebAPI.Dtos.Deficiente;
using AcessibilidadeWebAPI.Repositorios.Deficientes;
using AcessibilidadeWebAPI.Requisicoes.Deficiente;
using AcessibilidadeWebAPI.Resultados.Deficiente;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Deficientes
{
    public class ListarDeficienteExecutor : IRequestHandler<ListarDeficienteRequisicao, ListarDeficienteResultado>
    {
        private readonly IMapper mapper;
        private readonly IDeficienteRepositorio deficienteRepositorio;

        public ListarDeficienteExecutor(IMapper mapper, IDeficienteRepositorio deficienteRepositorio)
        {
            this.mapper = mapper;
            this.deficienteRepositorio = deficienteRepositorio;
        }
        public Task<ListarDeficienteResultado> Handle(ListarDeficienteRequisicao request, CancellationToken cancellationToken)
        {
            IQueryable<Entidades.Deficiente> arrDeficientes = deficienteRepositorio.Listar();

            DeficienteDto[] arrDeficientesDto = mapper.ProjectTo<DeficienteDto>(arrDeficientes).ToArray();

            return Task.FromResult(new ListarDeficienteResultado()
            {
                ArrDeficiente = arrDeficientesDto,
            });
        }
    }
}
