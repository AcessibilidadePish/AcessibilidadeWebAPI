using AcessibilidadeWebAPI.Resultados.Assistencias;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Assistencias
{
    public class EditarAssistenciaRequisicao : IRequest<EditarAssistenciaResultado>
    {
        public int IdAssistencia { get; set; }
        public int IdSolicitacaoAjuda { get; set; }
        public int IdUsuario { get; set; }
        public DateTimeOffset DataAceite { get; set; }
        public DateTimeOffset? DataConclusao { get; set; }
    }
}
