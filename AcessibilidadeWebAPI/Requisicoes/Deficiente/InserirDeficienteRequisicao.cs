using AcessibilidadeWebAPI.Resultados.Deficiente;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Deficiente
{
    public class InserirDeficienteRequisicao : IRequest<InserirDeficienteResultado>
    {
        public int IdUsuario { get; set; }
        public int TipoDeficiencia { get; set; }
    }
}
