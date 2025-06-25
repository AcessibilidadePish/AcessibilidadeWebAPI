using AcessibilidadeWebAPI.Repositorios.Locals;
using AcessibilidadeWebAPI.Requisicoes.Locals;
using AcessibilidadeWebAPI.Resultados.Locals;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Locals
{
    public class EditarLocalExecutor : IRequestHandler<EditarLocalRequisicao, EditarLocalResultado>
    {
        private readonly ILocalRepositorio localRepositorio;
        private readonly IMapper mapper;

        public EditarLocalExecutor(ILocalRepositorio localRepositorio, IMapper mapper)
        {
            this.localRepositorio = localRepositorio;
            this.mapper = mapper;
        }
        public Task<EditarLocalResultado> Handle(EditarLocalRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.Local Local = localRepositorio.ObterPorId(request.IdLocal);

            mapper.Map(request, Local);
            localRepositorio.Editar(Local);

            return Task.FromResult(new EditarLocalResultado()
            {
                IdLocal = request.IdLocal,
            });
        }
    }
}
