using AcessibilidadeWebAPI.BancoDados;
using AcessibilidadeWebAPI.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.Usuarios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AcessibilidadeDbContext _context;

        public UsuarioRepositorio(AcessibilidadeDbContext context)
        {
            _context = context;
        }

        public Usuario ObterPorId(params object[] ids) =>
            _context.Set<Usuario>().Find(ids);

        public IQueryable<Usuario> Listar(Expression<Func<Usuario, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<Usuario>().AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public IQueryable<Usuario> ListarIgnorandoFiltros(Expression<Func<Usuario, bool>>? predicate = null, bool tracking = false)
        {
            var query = _context.Set<Usuario>().IgnoreQueryFilters();
            if (!tracking)
                query = query.AsNoTracking();
            if (predicate != null)
                query = query.Where(predicate);
            return query;
        }

        public Usuario Inserir(Usuario model)
        {
            _context.Set<Usuario>().Add(model);
            _context.SaveChanges();
            return model;
        }

        public int Editar(Usuario model)
        {
            _context.Set<Usuario>().Update(model);
            return _context.SaveChanges();
        }

        public int Editar(Usuario model, Expression<Func<Usuario, object>>[] properties)
        {
            var entry = _context.Entry(model);
            entry.State = EntityState.Unchanged;
            foreach (var property in properties)
                entry.Property(property).IsModified = true;
            return _context.SaveChanges();
        }

        public int Deletar(Usuario model)
        {
            _context.Set<Usuario>().Remove(model);
            return _context.SaveChanges();
        }

        public int Deletar(params object[] ids)
        {
            var entity = _context.Set<Usuario>().Find(ids);
            if (entity == null) return 0;
            _context.Set<Usuario>().Remove(entity);
            return _context.SaveChanges();
        }

        public int EditarMultiplos(IEnumerable<Usuario> models, Expression<Func<Usuario, object>>[]? properties = null)
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

        public int DeletarMultiplos(IEnumerable<Usuario> models)
        {
            _context.Set<Usuario>().RemoveRange(models);
            return _context.SaveChanges();
        }

        public int InserirMultiplos(IEnumerable<Usuario> models)
        {
            _context.Set<Usuario>().AddRange(models);
            return _context.SaveChanges();
        }
    }
}