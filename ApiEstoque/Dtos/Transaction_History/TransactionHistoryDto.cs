namespace ApiEstoque.Dtos.Transaction_History
{
    public class TransactionHistoryDto
    {
        public int Id { get; set; }
        public string reason { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
