using System.ComponentModel.DataAnnotations;

namespace AcessibilidadeWebAPI.Models.Auth
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "Senha deve ter pelo menos 6 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Tipo de usuário é obrigatório")]
        public TipoUsuario TipoUsuario { get; set; }

        // Campo opcional: só preenchido se TipoUsuario = Deficiente
        public int? TipoDeficiencia { get; set; }

        // Campo opcional: só preenchido se TipoUsuario = Voluntario  
        public bool? Disponivel { get; set; } = true;

        public string NumeroSerie { get; set; } = null;
    }

    public enum TipoUsuario
    {
        Voluntario = 1,
        Deficiente = 2
    }
} 