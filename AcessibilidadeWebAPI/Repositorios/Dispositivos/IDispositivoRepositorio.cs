using AcessibilidadeWebAPI.Entidades;
using System.Linq.Expressions;

namespace AcessibilidadeWebAPI.Repositorios.Dispositivos
{
    public interface IDispositivoRepositorio
    {
        Dispositivo ObterPorId(params object[] ids);

        IQueryable<Dispositivo> Listar(Expression<Func<Dispositivo, bool>>? predicate = null, bool Tracking = false);

        IQueryable<Dispositivo> ListarIgnorandoFiltros(Expression<Func<Dispositivo, bool>>? predicate = null, bool Tracking = false);

        Dispositivo Inserir(Dispositivo model);

        int Editar(Dispositivo model);

        int Editar(Dispositivo model, params Expression<Func<Dispositivo, object>>[] properties);

        int Deletar(Dispositivo model);

        int Deletar(params object[] ids);

        int EditarMultiplos(IEnumerable<Dispositivo> models, params Expression<Func<Dispositivo, object>>[] properties);

        int DeletarMultiplos(IEnumerable<Dispositivo> models);

        int InserirMultiplos(IEnumerable<Dispositivo> models);
    }
}
