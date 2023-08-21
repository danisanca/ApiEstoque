namespace ApiEstoque.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Status { get; set; }
        private DateTime _createAt { get; set; }
        public DateTime CreateAt {
            get { return _createAt; }
            set {_createAt = value == null ? DateTime.UtcNow:value ; }
        }
        public DateTime? UpdateAt { get; set; }
        public int ShopId { get; set; }
        public ShopModel Shop { get; set; }
        

    }
}
