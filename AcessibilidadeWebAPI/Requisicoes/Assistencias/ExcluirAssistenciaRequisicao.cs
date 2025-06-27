using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Assistencias
{
    public class ExcluirAssistenciaRequisicao : IRequest
    {
        public int IdAssistencia { get; set; }

    }
}
