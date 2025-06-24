using AcessibilidadeWebAPI.BancoDados;
using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.Locals
{
    public class LocalRepositorio : ILocalRepositorio
    {
        private readonly AcessibilidadeDbContext _context;

        public LocalRepositorio(AcessibilidadeDbContext context)
        {
            _context = context;
        }

        public Local ObterPorId(params object[] ids) =>
            _context.Set<Local>().Find(ids);

        public IQueryable<Local> Listar(Expression<Func<Local, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<Local>().AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public IQueryable<Local> ListarIgnorandoFiltros(Expression<Func<Local, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<Local>().IgnoreQueryFilters();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Local Inserir(Local model)
        {
            _context.Set<Local>().Add(model);
            _context.SaveChanges();
            return model;
        }

        public int Editar(Local model)
        {
            _context.Set<Local>().Update(model);
            return _context.SaveChanges();
        }

        public int Editar(Local model, Expression<Func<Local, object>>[] properties)
        {
            var entry = _context.Entry(model);
            entry.State = EntityState.Unchanged;
            foreach (var property in properties)
                entry.Property(property).IsModified = true;
            return _context.SaveChanges();
        }

        public int Deletar(Local model)
        {
            _context.Set<Local>().Remove(model);
            return _context.SaveChanges();
        }

        public int Deletar(params object[] ids)
        {
            var entity = _context.Set<Local>().Find(ids);
            if (entity == null) return 0;
            _context.Set<Local>().Remove(entity);
            return _context.SaveChanges();
        }

        public int EditarMultiplos(IEnumerable<Local> models, Expression<Func<Local, object>>[]? properties = null)
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

        public int DeletarMultiplos(IEnumerable<Local> models)
        {
            _context.Set<Local>().RemoveRange(models);
            return _context.SaveChanges();
        }

        public int InserirMultiplos(IEnumerable<Local> models)
        {
            _context.Set<Local>().AddRange(models);
            return _context.SaveChanges();
        }
    }
}
