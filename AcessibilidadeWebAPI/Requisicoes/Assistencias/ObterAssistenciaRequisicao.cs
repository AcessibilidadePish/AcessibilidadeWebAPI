using AcessibilidadeWebAPI.Resultados.Assistencias;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Assistencias
{
    public class ObterAssistenciaRequisicao : IRequest<ObterAssistenciaResultado>
    {
        public int IdAssistencia { get; set; }
    }
}
