using AcessibilidadeWebAPI.Repositorios.AvaliacaoAvaliacaoLocals;
using AcessibilidadeWebAPI.Repositorios.Locals;
using AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals;
using AcessibilidadeWebAPI.Resultados.AvaliacaoLocals;
using AcessibilidadeWebAPI.Resultados.Locals;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.AvaliacaoLocals
{
    public class EditarAvaliacaoLocalExecutor : IRequestHandler<EditarAvaliacaoLocalRequisicao, EditarAvaliacaoLocalResultado>
    {
        private readonly IAvaliacaoLocalRepositorio avaliacaoLocalRepositorio;
        private readonly IMapper mapper;

        public EditarAvaliacaoLocalExecutor(IMapper mapper, IAvaliacaoLocalRepositorio avaliacaoLocalRepositorio)
        {
            this.mapper = mapper;
            this.avaliacaoLocalRepositorio = avaliacaoLocalRepositorio;
        }

        public Task<EditarAvaliacaoLocalResultado> Handle(EditarAvaliacaoLocalRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.AvaliacaoLocal avaliacaolocal = avaliacaoLocalRepositorio.ObterPorId(request.IdAvaliacaoLocal);

            mapper.Map(request, avaliacaolocal);
            avaliacaoLocalRepositorio.Editar(avaliacaolocal);

            return Task.FromResult(new EditarAvaliacaoLocalResultado()
            {
                IdAvaliacaoLocal = request.IdAvaliacaoLocal,
            });
        }
    }
}
