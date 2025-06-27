using AcessibilidadeWebAPI.Resultados.Assistencias;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Assistencias
{
    public class ListarAssistenciaRequisicao : IRequest<ListarAssistenciaResultado>
    {
        public int IdSolicitacaoAjuda { get; set; }
        public int IdUsuario { get; set; }
    }
}
