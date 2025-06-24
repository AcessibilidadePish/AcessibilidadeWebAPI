using AcessibilidadeWebAPI.Resultados.Deficiente;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Deficiente
{
    public class EditarDeficienteRequisicao : IRequest<EditarDeficienteResultado>
    {
        public int IdUsuario { get; set; }
        public int TipoDeficiencia { get; set; }
    }
}
