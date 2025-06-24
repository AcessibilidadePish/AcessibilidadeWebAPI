using AcessibilidadeWebAPI.Repositorios.Deficientes;
using AcessibilidadeWebAPI.Requisicoes.Deficiente;
using AcessibilidadeWebAPI.Resultados.Deficiente;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Deficientes
{
    public class EditarDeficienteExecutor : IRequestHandler<EditarDeficienteRequisicao, EditarDeficienteResultado>
    {
        private readonly IDeficienteRepositorio deficienteRepositorio;
        private readonly IMapper mapper;

        public EditarDeficienteExecutor(IDeficienteRepositorio deficienteRepositorio, IMapper mapper)
        {
            this.deficienteRepositorio = deficienteRepositorio;
            this.mapper = mapper;
        }
        public Task<EditarDeficienteResultado> Handle(EditarDeficienteRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.Deficiente deficiente = deficienteRepositorio.ObterPorId(request.IdUsuario);

            mapper.Map(request, deficiente);
            deficienteRepositorio.Editar(deficiente);

            return Task.FromResult(new EditarDeficienteResultado()
            {
                IdUsuario = request.IdUsuario,
            });
        }
    }
}
