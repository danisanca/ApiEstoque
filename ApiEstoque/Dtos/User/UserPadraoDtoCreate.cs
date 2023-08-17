using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dtos.User
{
    public class UserPadraoDtoCreate
    {
        [StringLength(45, ErrorMessage = "Nome deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Nome é um campo obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é um campo obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato do email é invalido.")]
        [StringLength(45, ErrorMessage = "Email deve ter no maximo {1} characters.")]
        public string Email { get; set; }

        [StringLength(12, ErrorMessage = "Nome deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Senha é um campo obrigatório.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Id da loja é um campo obrigatório.")]
        public int ShopId { get; set; }

        [Required(ErrorMessage = "Id da loja é um campo obrigatório.")]
        public int OfficeId { get; set; }
    }
}
