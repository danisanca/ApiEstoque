namespace ApiEstoque.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Status { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int ShopId { get; set; }
    }
}
