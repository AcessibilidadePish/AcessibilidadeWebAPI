using AcessibilidadeWebAPI.Entidades;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.Usuarios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly IDatabaseConnection<Usuario> databaseConnection;

        public UsuarioRepositorio(IDatabaseConnection<Usuario> databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public Usuario ObterPorId(params object[] ids) =>
             databaseConnection.ObterPorId(ids);

        public IQueryable<Usuario> Listar(Expression<Func<Usuario, bool>>? predicate = null, bool Tracking = false) =>
              databaseConnection.Listar(predicate, Tracking);

        public IQueryable<Usuario> ListarIgnorandoFiltros(Expression<Func<Usuario, bool>>? predicate = null, bool Tracking = false) =>
              databaseConnection.ListarIgnorandoFiltros(predicate, Tracking);

        public Usuario Inserir(Usuario model) =>
             databaseConnection.Inserir(model);

        public int Editar(Usuario model) =>
            databaseConnection.Editar(model);

        public int Editar(Usuario model, Expression<Func<Usuario, object>>[] properties) =>
            databaseConnection.Editar(model, properties);

        public int Deletar(Usuario model) =>
            databaseConnection.Excluir(model);

        public int Deletar(params object[] ids) =>
            databaseConnection.Excluir(ids);

        public int EditarMultiplos(IEnumerable<Usuario> models, Expression<Func<Usuario, object>>[]? properties = null) =>
            databaseConnection.EditarMultiplos(models, properties);

        public int DeletarMultiplos(IEnumerable<Usuario> models) =>
            databaseConnection.DeletarMultiplos(models);

        public int InserirMultiplos(IEnumerable<Usuario> models) =>
            databaseConnection.InserirMultiplos(models);
    }
}
