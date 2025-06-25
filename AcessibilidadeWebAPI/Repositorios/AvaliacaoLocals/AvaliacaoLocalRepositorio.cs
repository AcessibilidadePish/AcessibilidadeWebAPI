using AcessibilidadeWebAPI.BancoDados;
using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.AvaliacaoAvaliacaoLocals
{
    public class AvaliacaoLocalRepositorio : IAvaliacaoLocalRepositorio
    {
        private readonly AcessibilidadeDbContext _context;

        public AvaliacaoLocalRepositorio(AcessibilidadeDbContext context)
        {
            _context = context;
        }

        public AvaliacaoLocal ObterPorId(params object[] ids) =>
            _context.Set<AvaliacaoLocal>().Find(ids);

        public IQueryable<AvaliacaoLocal> Listar(Expression<Func<AvaliacaoLocal, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<AvaliacaoLocal>().AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public IQueryable<AvaliacaoLocal> ListarIgnorandoFiltros(Expression<Func<AvaliacaoLocal, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<AvaliacaoLocal>().IgnoreQueryFilters();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public AvaliacaoLocal Inserir(AvaliacaoLocal model)
        {
            _context.Set<AvaliacaoLocal>().Add(model);
            _context.SaveChanges();
            return model;
        }

        public int Editar(AvaliacaoLocal model)
        {
            _context.Set<AvaliacaoLocal>().Update(model);
            return _context.SaveChanges();
        }

        public int Editar(AvaliacaoLocal model, Expression<Func<AvaliacaoLocal, object>>[] properties)
        {
            var entry = _context.Entry(model);
            entry.State = EntityState.Unchanged;
            foreach (var property in properties)
                entry.Property(property).IsModified = true;
            return _context.SaveChanges();
        }

        public int Deletar(AvaliacaoLocal model)
        {
            _context.Set<AvaliacaoLocal>().Remove(model);
            return _context.SaveChanges();
        }

        public int Deletar(params object[] ids)
        {
            var entity = _context.Set<AvaliacaoLocal>().Find(ids);
            if (entity == null) return 0;
            _context.Set<AvaliacaoLocal>().Remove(entity);
            return _context.SaveChanges();
        }

        public int EditarMultiplos(IEnumerable<AvaliacaoLocal> models, Expression<Func<AvaliacaoLocal, object>>[]? properties = null)
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

        public int DeletarMultiplos(IEnumerable<AvaliacaoLocal> models)
        {
            _context.Set<AvaliacaoLocal>().RemoveRange(models);
            return _context.SaveChanges();
        }

        public int InserirMultiplos(IEnumerable<AvaliacaoLocal> models)
        {
            _context.Set<AvaliacaoLocal>().AddRange(models);
            return _context.SaveChanges();
        }
    }
}
