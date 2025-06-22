using AcessibilidadeWebAPI.BancoDados;
using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.Voluntarios
{
    public class VoluntarioRepositorio : IVoluntarioRepositorio
    {
        private readonly AcessibilidadeDbContext _context;

        public VoluntarioRepositorio(AcessibilidadeDbContext context)
        {
            _context = context;
        }

        public Voluntario ObterPorId(params object[] ids) =>
            _context.Set<Voluntario>().Find(ids);

        public IQueryable<Voluntario> Listar(Expression<Func<Voluntario, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<Voluntario>().AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public IQueryable<Voluntario> ListarIgnorandoFiltros(Expression<Func<Voluntario, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<Voluntario>().IgnoreQueryFilters();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Voluntario Inserir(Voluntario model)
        {
            _context.Set<Voluntario>().Add(model);
            _context.SaveChanges();
            return model;
        }

        public int Editar(Voluntario model)
        {
            _context.Set<Voluntario>().Update(model);
            return _context.SaveChanges();
        }

        public int Editar(Voluntario model, Expression<Func<Voluntario, object>>[] properties)
        {
            var entry = _context.Entry(model);
            entry.State = EntityState.Unchanged;
            foreach (var property in properties)
                entry.Property(property).IsModified = true;
            return _context.SaveChanges();
        }

        public int Deletar(Voluntario model)
        {
            _context.Set<Voluntario>().Remove(model);
            return _context.SaveChanges();
        }

        public int Deletar(params object[] ids)
        {
            var entity = _context.Set<Voluntario>().Find(ids);
            if (entity == null) return 0;
            _context.Set<Voluntario>().Remove(entity);
            return _context.SaveChanges();
        }

        public int EditarMultiplos(IEnumerable<Voluntario> models, Expression<Func<Voluntario, object>>[]? properties = null)
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

        public int DeletarMultiplos(IEnumerable<Voluntario> models)
        {
            _context.Set<Voluntario>().RemoveRange(models);
            return _context.SaveChanges();
        }

        public int InserirMultiplos(IEnumerable<Voluntario> models)
        {
            _context.Set<Voluntario>().AddRange(models);
            return _context.SaveChanges();
        }
    }
}
