using AcessibilidadeWebAPI.Entidades;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.AvaliacaoAvaliacaoLocals
{
    public interface IAvaliacaoLocalRepositorio
    {
        AvaliacaoLocal ObterPorId(params object[] ids);

        IQueryable<AvaliacaoLocal> Listar(Expression<Func<AvaliacaoLocal, bool>>? predicate = null, bool Tracking = false);

        IQueryable<AvaliacaoLocal> ListarIgnorandoFiltros(Expression<Func<AvaliacaoLocal, bool>>? predicate = null, bool Tracking = false);

        AvaliacaoLocal Inserir(AvaliacaoLocal model);

        int Editar(AvaliacaoLocal model);

        int Editar(AvaliacaoLocal model, params Expression<Func<AvaliacaoLocal, object>>[] properties);

        int Deletar(AvaliacaoLocal model);

        int Deletar(params object[] ids);

        int EditarMultiplos(IEnumerable<AvaliacaoLocal> models, params Expression<Func<AvaliacaoLocal, object>>[] properties);

        int DeletarMultiplos(IEnumerable<AvaliacaoLocal> models);

        int InserirMultiplos(IEnumerable<AvaliacaoLocal> models);
    }
}
