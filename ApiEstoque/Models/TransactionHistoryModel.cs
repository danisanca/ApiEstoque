namespace ApiEstoque.Models
{
    public class TransactionHistoryModel
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        private DateTime _createAt { get; set; }
        public DateTime CreateAt
        {
            get { return _createAt; }
            set { _createAt = value == null ? DateTime.UtcNow : value; }
        }

        public int ProductId { get; set; }
        public ProductModel Product { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
