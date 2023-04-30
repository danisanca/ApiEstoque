namespace ApiEstoque.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public int Amount { get; set; }
        public int ProductId { get; set; }
        public string Status { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }
    }
}
