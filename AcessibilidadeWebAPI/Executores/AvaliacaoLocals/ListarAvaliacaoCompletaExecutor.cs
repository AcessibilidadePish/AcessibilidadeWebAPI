using AcessibilidadeWebAPI.Dtos.AvaliacaoLocal;
using AcessibilidadeWebAPI.Dtos.Local;
using AcessibilidadeWebAPI.Dtos.Usuario;
using AcessibilidadeWebAPI.Repositorios.AvaliacaoAvaliacaoLocals;
using AcessibilidadeWebAPI.Requisicoes.AvaliacaoLocals;
using AcessibilidadeWebAPI.Resultados.AvaliacaoLocals;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AcessibilidadeWebAPI.Executores.AvaliacaoLocals
{
    public class ListarAvaliacaoCompletaExecutor : IRequestHandler<ListarAvaliacaoCompletaRequisicao, ListarAvaliacaoCompletaResultado>
    {
        private readonly IAvaliacaoLocalRepositorio avaliacaoLocalRepositorio;
        private readonly IMapper mapper;

        public ListarAvaliacaoCompletaExecutor(IAvaliacaoLocalRepositorio avaliacaoLocalRepositorio, IMapper mapper)
        {
            this.avaliacaoLocalRepositorio = avaliacaoLocalRepositorio;
            this.mapper = mapper;
        }

        public async Task<ListarAvaliacaoCompletaResultado> Handle(ListarAvaliacaoCompletaRequisicao request, CancellationToken cancellationToken)
        {
            var query = avaliacaoLocalRepositorio.Listar()
                .Include(a => a.LocalNavigation)
                .Include(a => a.DispositivoNavigation)
                .ThenInclude(d => d.UsuarioProprietarioNavigation)
                .AsQueryable();

            // Aplicar filtros se fornecidos
            if (request.LocalId.HasValue)
            {
                query = query.Where(a => a.LocalId == request.LocalId.Value);
            }

            if (request.UsuarioId.HasValue)
            {
                query = query.Where(a => a.DispositivoNavigation.UsuarioProprietarioId == request.UsuarioId.Value);
            }

            if (request.Acessivel.HasValue)
            {
                query = query.Where(a => a.Acessivel == request.Acessivel.Value);
            }

            if (request.DataInicio.HasValue)
            {
                query = query.Where(a => a.Timestamp >= request.DataInicio.Value);
            }

            if (request.DataFim.HasValue)
            {
                query = query.Where(a => a.Timestamp <= request.DataFim.Value);
            }

            // Contar total antes da paginação
            var total = await query.CountAsync(cancellationToken);

            // Aplicar paginação
            var avaliacoes = await query
                .OrderByDescending(a => a.Timestamp)
                .Skip((request.Pagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync(cancellationToken);

            // Mapear para DTOs
            var avaliacoesDto = avaliacoes.Select(a => new AvaliacaoLocalCompletaDto
            {
                Id = a.Id,
                LocalId = a.LocalId,
                DispositivoId = a.DispositivoId,
                Acessivel = a.Acessivel,
                Observacoes = a.Observacoes ?? string.Empty,
                Timestamp = a.Timestamp,
                Local = a.LocalNavigation != null ? new LocalDto
                {
                    IdLocal = a.LocalNavigation.IdLocal,
                    Descricao = a.LocalNavigation.Descricao,
                    Latitude = a.LocalNavigation.Latitude,
                    Longitude = a.LocalNavigation.Longitude,
                    AvaliacaoAcessibilidade = a.LocalNavigation.AvaliacaoAcessibilidade
                } : null,
                Usuario = a.DispositivoNavigation?.UsuarioProprietarioNavigation != null ? new UsuarioDto
                {
                    IdUsuario = a.DispositivoNavigation.UsuarioProprietarioNavigation.IdUsuario,
                    Nome = a.DispositivoNavigation.UsuarioProprietarioNavigation.Nome,
                    Email = a.DispositivoNavigation.UsuarioProprietarioNavigation.Email,
                    Telefone = a.DispositivoNavigation.UsuarioProprietarioNavigation.Telefone
                } : null,
                Dispositivo = a.DispositivoNavigation != null ? new DispositivoDto
                {
                    Id = a.DispositivoNavigation.Id,
                    NumeroSerie = a.DispositivoNavigation.NumeroSerie,
                    DataRegistro = a.DispositivoNavigation.DataRegistro,
                    UsuarioProprietarioId = a.DispositivoNavigation.UsuarioProprietarioId
                } : null
            }).ToList();

            var temProximaPagina = (request.Pagina * request.TamanhoPagina) < total;

            return new ListarAvaliacaoCompletaResultado
            {
                AvaliacoesCompletas = avaliacoesDto,
                Total = total,
                PaginaAtual = request.Pagina,
                TamanhoPagina = request.TamanhoPagina,
                TemProximaPagina = temProximaPagina
            };
        }
    }
} 