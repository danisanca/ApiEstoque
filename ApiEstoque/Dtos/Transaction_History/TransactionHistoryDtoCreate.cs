using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dtos.Transaction_History
{
    public class TransactionHistoryDtoCreate
    {
        [StringLength(45, ErrorMessage = "O motivo deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Motivo é um campo obrigatório.")]
        public string reason { get; set; }

        [Required(ErrorMessage = "Id do produto é um campo obrigatório.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Id do usuario é um campo obrigatório.")]
        public int UserId { get; set; }
    }
}
