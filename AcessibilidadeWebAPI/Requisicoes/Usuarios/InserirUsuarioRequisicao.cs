using AcessibilidadeWebAPI.Resultados.Usuarios;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Usuarios
{
    public class InserirUsuarioRequisicao : IRequest<InserirUsuarioResultado>
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Telefone { get; set; }
        public bool ehDeficiente { get; set; }
    }
}
