using AcessibilidadeWebAPI.BancoDados;
using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.Dispositivos
{
    public class DispositivoRepositorio : IDispositivoRepositorio
    {
        private readonly AcessibilidadeDbContext _context;

        public DispositivoRepositorio(AcessibilidadeDbContext context)
        {
            _context = context;
        }

        public Dispositivo ObterPorId(params object[] ids) =>
            _context.Set<Dispositivo>().Find(ids);

        public IQueryable<Dispositivo> Listar(Expression<Func<Dispositivo, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<Dispositivo>().AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public IQueryable<Dispositivo> ListarIgnorandoFiltros(Expression<Func<Dispositivo, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<Dispositivo>().IgnoreQueryFilters();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Dispositivo Inserir(Dispositivo model)
        {
            _context.Set<Dispositivo>().Add(model);
            _context.SaveChanges();
            return model;
        }

        public int Editar(Dispositivo model)
        {
            _context.Set<Dispositivo>().Update(model);
            return _context.SaveChanges();
        }

        public int Editar(Dispositivo model, Expression<Func<Dispositivo, object>>[] properties)
        {
            var entry = _context.Entry(model);
            entry.State = EntityState.Unchanged;
            foreach (var property in properties)
                entry.Property(property).IsModified = true;
            return _context.SaveChanges();
        }

        public int Deletar(Dispositivo model)
        {
            _context.Set<Dispositivo>().Remove(model);
            return _context.SaveChanges();
        }

        public int Deletar(params object[] ids)
        {
            var entity = _context.Set<Dispositivo>().Find(ids);
            if (entity == null) return 0;
            _context.Set<Dispositivo>().Remove(entity);
            return _context.SaveChanges();
        }

        public int EditarMultiplos(IEnumerable<Dispositivo> models, Expression<Func<Dispositivo, object>>[]? properties = null)
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

        public int DeletarMultiplos(IEnumerable<Dispositivo> models)
        {
            _context.Set<Dispositivo>().RemoveRange(models);
            return _context.SaveChanges();
        }

        public int InserirMultiplos(IEnumerable<Dispositivo> models)
        {
            _context.Set<Dispositivo>().AddRange(models);
            return _context.SaveChanges();
        }
    }
}
