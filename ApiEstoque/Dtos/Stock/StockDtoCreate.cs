using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dtos.Stock
{
    public class StockDtoCreate
    {
        [StringLength(255, ErrorMessage = "O codigo de barra deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Codigo de barra é um campo obrigatório.")]
        public string Barcode { get; set; }

        [Required(ErrorMessage = "Quantidade é um campo obrigatório.")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Id do produto é um campo obrigatório.")]
        public int ProductId { get; set; }


        [Required(ErrorMessage = "Id da loja é um campo obrigatório.")]
        public int ShopId { get; set; }
    }
}
