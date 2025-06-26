using AcessibilidadeWebAPI.BancoDados;
using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.SolicitacaoAjudas
{
    public class SolicitacaoAjudaRepositorio : ISolicitacaoAjudaRepositorio
    {
        private readonly AcessibilidadeDbContext _context;

        public SolicitacaoAjudaRepositorio(AcessibilidadeDbContext context)
        {
            _context = context;
        }

        public SolicitacaoAjuda ObterPorId(params object[] ids) =>
            _context.Set<SolicitacaoAjuda>().Find(ids);

        public IQueryable<SolicitacaoAjuda> Listar(Expression<Func<SolicitacaoAjuda, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<SolicitacaoAjuda>().AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public IQueryable<SolicitacaoAjuda> ListarIgnorandoFiltros(Expression<Func<SolicitacaoAjuda, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<SolicitacaoAjuda>().IgnoreQueryFilters();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public SolicitacaoAjuda Inserir(SolicitacaoAjuda model)
        {
            _context.Set<SolicitacaoAjuda>().Add(model);
            _context.SaveChanges();
            return model;
        }

        public int Editar(SolicitacaoAjuda model)
        {
            _context.Set<SolicitacaoAjuda>().Update(model);
            return _context.SaveChanges();
        }

        public int Editar(SolicitacaoAjuda model, Expression<Func<SolicitacaoAjuda, object>>[] properties)
        {
            var entry = _context.Entry(model);
            entry.State = EntityState.Unchanged;
            foreach (var property in properties)
                entry.Property(property).IsModified = true;
            return _context.SaveChanges();
        }

        public int Deletar(SolicitacaoAjuda model)
        {
            _context.Set<SolicitacaoAjuda>().Remove(model);
            return _context.SaveChanges();
        }

        public int Deletar(params object[] ids)
        {
            var entity = _context.Set<SolicitacaoAjuda>().Find(ids);
            if (entity == null) return 0;
            _context.Set<SolicitacaoAjuda>().Remove(entity);
            return _context.SaveChanges();
        }

        public int EditarMultiplos(IEnumerable<SolicitacaoAjuda> models, Expression<Func<SolicitacaoAjuda, object>>[]? properties = null)
        {
            foreach (var model in models)
            {
                var entry = _context.Entry(model);
                entry.State = EntityState.Unchanged;
                if (properties == null)
                    entry.State = EntityState.Modified;
                else
                    foreach (var prop in properties)
                        entry.Property(prop).IsModified = true;
            }
            return _context.SaveChanges();
        }

        public int DeletarMultiplos(IEnumerable<SolicitacaoAjuda> models)
        {
            _context.Set<SolicitacaoAjuda>().RemoveRange(models);
            return _context.SaveChanges();
        }

        public int InserirMultiplos(IEnumerable<SolicitacaoAjuda> models)
        {
            _context.Set<SolicitacaoAjuda>().AddRange(models);
            return _context.SaveChanges();
        }
    }
}
