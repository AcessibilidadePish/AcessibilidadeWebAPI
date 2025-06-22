using AcessibilidadeWebAPI.Entidades;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.Deficientes
{
    public interface IDeficienteRepositorio
    {
        Deficiente ObterPorId(params object[] ids);

        IQueryable<Deficiente> Listar(Expression<Func<Deficiente, bool>>? predicate = null, bool Tracking = false);

        IQueryable<Deficiente> ListarIgnorandoFiltros(Expression<Func<Deficiente, bool>>? predicate = null, bool Tracking = false);

        Deficiente Inserir(Deficiente model);

        int Editar(Deficiente model);

        int Editar(Deficiente model, params Expression<Func<Deficiente, object>>[] properties);

        int Deletar(Deficiente model);

        int Deletar(params object[] ids);

        int EditarMultiplos(IEnumerable<Deficiente> models, params Expression<Func<Deficiente, object>>[] properties);

        int DeletarMultiplos(IEnumerable<Deficiente> models);

        int InserirMultiplos(IEnumerable<Deficiente> models);
    }
}
