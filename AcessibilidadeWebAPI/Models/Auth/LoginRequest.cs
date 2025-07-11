using System.ComponentModel.DataAnnotations;

namespace AcessibilidadeWebAPI.Models.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Senha { get; set; }
    }
} 