using AcessibilidadeWebAPI.Dtos.AvaliacaoLocal;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Repositorios.AvaliacaoAvaliacaoLocals;
using AcessibilidadeWebAPI.Repositorios.Dispositivos;
using AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals;
using AcessibilidadeWebAPI.Resultados.AvaliacaoLocals;
using AutoMapper;
using MediatR;

namespace AcessibilidadeWebAPI.Executores.AvaliacaoLocals
{
    public class InserirAvaliacaoLocalExecutor : IRequestHandler<InserirAvaliacaoLocalRequisicao, InserirAvaliacaoLocalResultado>
    {
        private readonly IAvaliacaoLocalRepositorio avaliacaoLocalRepositorio;
        private readonly IMapper mapper;
        private readonly IDispositivoRepositorio dispositivoRepositorio;

        public InserirAvaliacaoLocalExecutor(IMapper mapper, IAvaliacaoLocalRepositorio avaliacaoLocalRepositorio, IDispositivoRepositorio dispositivoRepositorio)
        {
            this.mapper = mapper;
            this.avaliacaoLocalRepositorio = avaliacaoLocalRepositorio;
            this.dispositivoRepositorio = dispositivoRepositorio;
        }

        public Task<InserirAvaliacaoLocalResultado> Handle(InserirAvaliacaoLocalRequisicao request, CancellationToken cancellationToken)
        {
            IQueryable<Dispositivo> disp = dispositivoRepositorio.Listar(a => a.UsuarioProprietarioId == request.IdUsuario);
            AvaliacaoLocal Local = mapper.Map<AvaliacaoLocal>(request);
            Local.DispositivoId = disp.FirstOrDefault()?.Id ?? throw new InvalidOperationException("Dispositivo não encontrado para o usuário especificado.");
            AvaliacaoLocal res = avaliacaoLocalRepositorio.Inserir(Local);

            return Task.FromResult(new InserirAvaliacaoLocalResultado()
            {
                AvaliacaoLocal = mapper.Map<AvaliacaoLocalDto>(res),
            });
        }
    }
}