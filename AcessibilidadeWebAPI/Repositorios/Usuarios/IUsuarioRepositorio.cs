using AcessibilidadeWebAPI.Entidades;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.Usuarios
{
    public interface IUsuarioRepositorio
    {
        Usuario ObterPorId(params object[] ids);

        IQueryable<Usuario> Listar(Expression<Func<Usuario, bool>>? predicate = null, bool Tracking = false);

        IQueryable<Usuario> ListarIgnorandoFiltros(Expression<Func<Usuario, bool>>? predicate = null, bool Tracking = false);

        Usuario Inserir(Usuario model);

        int Editar(Usuario model);

        int Editar(Usuario model, params Expression<Func<Usuario, object>>[] properties);

        int Deletar(Usuario model);

        int Deletar(params object[] ids);

        int EditarMultiplos(IEnumerable<Usuario> models, params Expression<Func<Usuario, object>>[] properties);

        int DeletarMultiplos(IEnumerable<Usuario> models);

        int InserirMultiplos(IEnumerable<Usuario> models);

        Task<Usuario> ObterUsuario(int id);
        Task<Usuario> ObterUsuarioPorEmail(string email);
        Task<Usuario> InserirUsuario(Usuario usuario);
    }
}
