using AcessibilidadeWebAPI.Resultados.Voluntario;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Voluntario
{
    public class InserirVoluntarioRequisicao : IRequest<InserirVoluntarioResultado>
    {
        public int IdUsuario { get; set; }
        public bool Disponivel { get; set; }
        public int Avaliacao { get; set; }
    }
}
