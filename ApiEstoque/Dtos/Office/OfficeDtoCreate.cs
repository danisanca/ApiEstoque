using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dtos.Office
{
    public class OfficeDtoCreate
    {
        [StringLength(45, ErrorMessage = "Nome deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Nome é um campo obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Id do usuario é um campo obrigatório.")]
        public int UserId { get; set; }
    }
}
