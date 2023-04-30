namespace ApiEstoque.Models
{
    public class StockModel
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
        private DateTime _createAt { get; set; }
        public DateTime CreateAt
        {
            get { return _createAt; }
            set { _createAt = value == null ? DateTime.UtcNow : value; }
        }
        public DateTime? UpdateAt { get; set; }

        public int ProductId { get; set; }
        public IEnumerable<ProductModel> Product { get; set; }
    }
}
