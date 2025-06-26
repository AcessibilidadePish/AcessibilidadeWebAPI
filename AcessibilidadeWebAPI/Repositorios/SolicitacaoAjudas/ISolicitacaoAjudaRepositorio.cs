using AcessibilidadeWebAPI.Entidades;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.SolicitacaoAjudas
{
    public interface ISolicitacaoAjudaRepositorio
    {
        SolicitacaoAjuda ObterPorId(params object[] ids);

        IQueryable<SolicitacaoAjuda> Listar(Expression<Func<SolicitacaoAjuda, bool>>? predicate = null, bool Tracking = false);

        IQueryable<SolicitacaoAjuda> ListarIgnorandoFiltros(Expression<Func<SolicitacaoAjuda, bool>>? predicate = null, bool Tracking = false);

        SolicitacaoAjuda Inserir(SolicitacaoAjuda model);

        int Editar(SolicitacaoAjuda model);

        int Editar(SolicitacaoAjuda model, params Expression<Func<SolicitacaoAjuda, object>>[] properties);

        int Deletar(SolicitacaoAjuda model);

        int Deletar(params object[] ids);

        int EditarMultiplos(IEnumerable<SolicitacaoAjuda> models, params Expression<Func<SolicitacaoAjuda, object>>[] properties);

        int DeletarMultiplos(IEnumerable<SolicitacaoAjuda> models);

        int InserirMultiplos(IEnumerable<SolicitacaoAjuda> models);
    }
}
