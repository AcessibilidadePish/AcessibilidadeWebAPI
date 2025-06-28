using AcessibilidadeWebAPI.Resultados.Voluntario;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Voluntario
{
    public class EditarVoluntarioRequisicao : IRequest<EditarVoluntarioResultado>
    {
        public int IdUsuario { get; set; }
        public bool Disponivel { get; set; }
        public decimal Avaliacao { get; set; }
    }
}
