using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dtos.Stock
{
    public class StockDtoUpdate
    {
        [Required(ErrorMessage = "Quantidade é um campo obrigatório.")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Quantidade é um campo obrigatório.")]
        public string? Status { get; set; }
    }
}
