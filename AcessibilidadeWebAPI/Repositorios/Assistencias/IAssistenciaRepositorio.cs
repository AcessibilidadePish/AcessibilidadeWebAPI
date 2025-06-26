using AcessibilidadeWebAPI.Entidades;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.Assistencias
{
    public interface IAssistenciaRepositorio
    {
        Assistencia ObterPorId(params object[] ids);

        IQueryable<Assistencia> Listar(Expression<Func<Assistencia, bool>>? predicate = null, bool Tracking = false);

        IQueryable<Assistencia> ListarIgnorandoFiltros(Expression<Func<Assistencia, bool>>? predicate = null, bool Tracking = false);

        Assistencia Inserir(Assistencia model);

        int Editar(Assistencia model);

        int Editar(Assistencia model, params Expression<Func<Assistencia, object>>[] properties);

        int Deletar(Assistencia model);

        int Deletar(params object[] ids);

        int EditarMultiplos(IEnumerable<Assistencia> models, params Expression<Func<Assistencia, object>>[] properties);

        int DeletarMultiplos(IEnumerable<Assistencia> models);

        int InserirMultiplos(IEnumerable<Assistencia> models);
    }
}
