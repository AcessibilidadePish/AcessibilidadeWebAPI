using AcessibilidadeWebAPI.Resultados.Usuarios;
using MediatR;

namespace AcessibilidadeWebAPI.Requisicoes.Usuarios
{
    public class EditarUsuarioRequisicao : IRequest<EditarUsuarioResultado>
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Telefone { get; set; }
        public string Senha { get; set; }
    }
}
