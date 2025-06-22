using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Voluntario
{
    public class ExcluirVoluntarioRequisicao : IRequest
    {
        public int IdUsuario { get; set; }

    }
}
