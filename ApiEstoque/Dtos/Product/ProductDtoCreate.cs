using System.ComponentModel.DataAnnotations;

namespace ApiEstoque.Dtos.Product
{
    public class ProductDtoCreate
    {
        [StringLength(45, ErrorMessage = "Nome deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Nome é um campo obrigatório.")]
        public string Name { get; set; }

        [StringLength(12,ErrorMessage = "O codígo do produto deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Codígo do produto é um campo obrigatório.")]
        public string ProductCode { get; set; }

        [StringLength(120, ErrorMessage = "A Descrição deve ter no maximo {1} characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Preço é um campo obrigatório.")]
        public double Price { get; set; }

        [StringLength(12, ErrorMessage = "A unidade de medida deve ter no maximo {1} characters.")]
        [Required(ErrorMessage = "Unidade de medida é um campo obrigatório.")]
        public string UnitOfMeasure { get; set; }


        [Required(ErrorMessage = "Id da loja é um campo obrigatório.")]
        public int ShopId { get; set; }
    }
}
