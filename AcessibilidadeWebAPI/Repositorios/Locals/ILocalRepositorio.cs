using AcessibilidadeWebAPI.Entidades;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.Locals
{
    public interface ILocalRepositorio
    {
        Local ObterPorId(params object[] ids);

        IQueryable<Local> Listar(Expression<Func<Local, bool>>? predicate = null, bool Tracking = false);

        IQueryable<Local> ListarIgnorandoFiltros(Expression<Func<Local, bool>>? predicate = null, bool Tracking = false);

        Local Inserir(Local model);

        int Editar(Local model);

        int Editar(Local model, params Expression<Func<Local, object>>[] properties);

        int Deletar(Local model);

        int Deletar(params object[] ids);

        int EditarMultiplos(IEnumerable<Local> models, params Expression<Func<Local, object>>[] properties);

        int DeletarMultiplos(IEnumerable<Local> models);

        int InserirMultiplos(IEnumerable<Local> models);
    }
}
