using System.Linq.Expressions;
using AcessibilidadeWebAPI.Entidades;

namespace AcessibilidadeWebAPI.Repositorios.Voluntarios
{
    public interface IVoluntarioRepositorio
    {
        Voluntario ObterPorId(params object[] ids);

        IQueryable<Voluntario> Listar(Expression<Func<Voluntario, bool>>? predicate = null, bool Tracking = false);

        IQueryable<Voluntario> ListarIgnorandoFiltros(Expression<Func<Voluntario, bool>>? predicate = null, bool Tracking = false);

        Voluntario Inserir(Voluntario model);

        int Editar(Voluntario model);

        int Editar(Voluntario model, params Expression<Func<Voluntario, object>>[] properties);

        int Deletar(Voluntario model);

        int Deletar(params object[] ids);

        int EditarMultiplos(IEnumerable<Voluntario> models, params Expression<Func<Voluntario, object>>[] properties);

        int DeletarMultiplos(IEnumerable<Voluntario> models);

        int InserirMultiplos(IEnumerable<Voluntario> models);
    }
}
