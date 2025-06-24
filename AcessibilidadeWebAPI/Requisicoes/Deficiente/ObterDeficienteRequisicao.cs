using AcessibilidadeWebAPI.Resultados.Deficiente;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Deficiente
{
    public class ObterDeficienteRequisicao : IRequest<ObterDeficienteResultado>
    {
        public int IdUsuario { get; set; }

    }
}
