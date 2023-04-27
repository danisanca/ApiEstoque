using ApiEstoque.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Necessário informar um email.")]
        [EmailAddress(ErrorMessage ="Formato do email invalido.")]
        [StringLength(45,ErrorMessage ="Email deve ter no maximo {1} characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Necessário informar uma senha.")]
        [StringLength(12, ErrorMessage = "Senha deve ter no maximo {1} characters.")]
        public string Password { get; set; }

        public void SetPasswordHash()
        {
            Password = Password.CreateHash();
        }
    }
}
