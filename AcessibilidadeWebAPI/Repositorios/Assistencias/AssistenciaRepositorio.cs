using AcessibilidadeWebAPI.BancoDados;
using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.Assistencias
{
    public class AssistenciaRepositorio : IAssistenciaRepositorio
    {
        private readonly AcessibilidadeDbContext _context;

        public AssistenciaRepositorio(AcessibilidadeDbContext context)
        {
            _context = context;
        }

        public Assistencia ObterPorId(params object[] ids) =>
            _context.Set<Assistencia>().Find(ids);

        public IQueryable<Assistencia> Listar(Expression<Func<Assistencia, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<Assistencia>().AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public IQueryable<Assistencia> ListarIgnorandoFiltros(Expression<Func<Assistencia, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<Assistencia>().IgnoreQueryFilters();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Assistencia Inserir(Assistencia model)
        {
            _context.Set<Assistencia>().Add(model);
            _context.SaveChanges();
            return model;
        }

        public int Editar(Assistencia model)
        {
            _context.Set<Assistencia>().Update(model);
            return _context.SaveChanges();
        }

        public int Editar(Assistencia model, Expression<Func<Assistencia, object>>[] properties)
        {
            var entry = _context.Entry(model);
            entry.State = EntityState.Unchanged;
            foreach (var property in properties)
                entry.Property(property).IsModified = true;
            return _context.SaveChanges();
        }

        public int Deletar(Assistencia model)
        {
            _context.Set<Assistencia>().Remove(model);
            return _context.SaveChanges();
        }

        public int Deletar(params object[] ids)
        {
            var entity = _context.Set<Assistencia>().Find(ids);
            if (entity == null) return 0;
            _context.Set<Assistencia>().Remove(entity);
            return _context.SaveChanges();
        }

        public int EditarMultiplos(IEnumerable<Assistencia> models, Expression<Func<Assistencia, object>>[]? properties = null)
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

        public int DeletarMultiplos(IEnumerable<Assistencia> models)
        {
            _context.Set<Assistencia>().RemoveRange(models);
            return _context.SaveChanges();
        }

        public int InserirMultiplos(IEnumerable<Assistencia> models)
        {
            _context.Set<Assistencia>().AddRange(models);
            return _context.SaveChanges();
        }
    }
}
