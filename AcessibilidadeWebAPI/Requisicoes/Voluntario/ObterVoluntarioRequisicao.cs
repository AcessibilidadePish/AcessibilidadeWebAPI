using AcessibilidadeWebAPI.Resultados.Usuarios;
using AcessibilidadeWebAPI.Resultados.Voluntario;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Voluntario
{
    public class ObterVoluntarioRequisicao : IRequest<ObterVoluntarioResultado>
    {
        public int IdUsuario { get; set; }

    }
}
