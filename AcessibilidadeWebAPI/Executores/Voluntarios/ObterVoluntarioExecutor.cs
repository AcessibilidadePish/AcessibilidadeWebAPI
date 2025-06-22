using AcessibilidadeWebAPI.Dtos.Usuario;
using AcessibilidadeWebAPI.Dtos.Voluntario;
using AcessibilidadeWebAPI.Repositorios.Usuarios;
using AcessibilidadeWebAPI.Repositorios.Voluntarios;
using AcessibilidadeWebAPI.Requisicoes.Usuarios;
using AcessibilidadeWebAPI.Requisicoes.Voluntario;
using AcessibilidadeWebAPI.Resultados.Usuarios;
using AcessibilidadeWebAPI.Resultados.Voluntario;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.Voluntarios
{
    public class ObterVoluntarioExecutor : IRequestHandler<ObterVoluntarioRequisicao, ObterVoluntarioResultado>
    {
        private readonly IMapper mapper;
        private readonly IVoluntarioRepositorio voluntarioRepositorio;

        public ObterVoluntarioExecutor(IMapper mapper, IVoluntarioRepositorio voluntarioRepositorio)
        {
            this.mapper = mapper;
            this.voluntarioRepositorio = voluntarioRepositorio;
        }

        public Task<ObterVoluntarioResultado> Handle(ObterVoluntarioRequisicao request, CancellationToken cancellationToken)
        {
            Entidades.Voluntario voluntario = voluntarioRepositorio.ObterPorId(request.IdUsuario);

            VoluntarioDto voluntarioDto = mapper.Map<VoluntarioDto>(voluntario);

            return Task.FromResult(new ObterVoluntarioResultado()
            {
                Voluntario = voluntarioDto,
            });
        }
    }
}
