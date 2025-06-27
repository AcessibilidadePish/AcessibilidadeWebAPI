using AcessibilidadeWebAPI.Resultados.Assistencias;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Assistencias
{
    public class InserirAssistenciaRequisicao : IRequest<InserirAssistenciaResultado>
    {
        public int IdSolicitacaoAjuda { get; set; }
        public int IdUsuario { get; set; }
        public DateTimeOffset DataAceite { get; set; }
        public DateTimeOffset DataConclusao { get; set; }
    }
}
