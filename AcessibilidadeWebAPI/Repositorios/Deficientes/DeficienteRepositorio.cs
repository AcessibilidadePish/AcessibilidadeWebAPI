using AcessibilidadeWebAPI.BancoDados;
using AcessibilidadeWebAPI.Entidades;
using AcessibilidadeWebAPI.Repositorios.Voluntarios;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.Deficientes
{
    public class DeficienteRepositorio : IDeficienteRepositorio
    {
        private readonly AcessibilidadeDbContext _context;

        public DeficienteRepositorio(AcessibilidadeDbContext context)
        {
            _context = context;
        }

        public Deficiente ObterPorId(params object[] ids) =>
            _context.Set<Deficiente>().Find(ids);

        public IQueryable<Deficiente> Listar(Expression<Func<Deficiente, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<Deficiente>().AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public IQueryable<Deficiente> ListarIgnorandoFiltros(Expression<Func<Deficiente, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<Deficiente>().IgnoreQueryFilters();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Deficiente Inserir(Deficiente model)
        {
            _context.Set<Deficiente>().Add(model);
            _context.SaveChanges();
            return model;
        }

        public int Editar(Deficiente model)
        {
            _context.Set<Deficiente>().Update(model);
            return _context.SaveChanges();
        }

        public int Editar(Deficiente model, Expression<Func<Deficiente, object>>[] properties)
        {
            var entry = _context.Entry(model);
            entry.State = EntityState.Unchanged;
            foreach (var property in properties)
                entry.Property(property).IsModified = true;
            return _context.SaveChanges();
        }

        public int Deletar(Deficiente model)
        {
            _context.Set<Deficiente>().Remove(model);
            return _context.SaveChanges();
        }

        public int Deletar(params object[] ids)
        {
            var entity = _context.Set<Deficiente>().Find(ids);
            if (entity == null) return 0;
            _context.Set<Deficiente>().Remove(entity);
            return _context.SaveChanges();
        }

        public int EditarMultiplos(IEnumerable<Deficiente> models, Expression<Func<Deficiente, object>>[]? properties = null)
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

        public int DeletarMultiplos(IEnumerable<Deficiente> models)
        {
            _context.Set<Deficiente>().RemoveRange(models);
            return _context.SaveChanges();
        }

        public int InserirMultiplos(IEnumerable<Deficiente> models)
        {
            _context.Set<Deficiente>().AddRange(models);
            return _context.SaveChanges();
        }
    }
}
