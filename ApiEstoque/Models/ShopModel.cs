namespace ApiEstoque.Models
{
    public class ShopModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        private DateTime _createAt;
        public DateTime CreateAt
        {
            get { return _createAt; }
            set
            {
                _createAt = value == null ? DateTime.UtcNow : value;
            }
        }
        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
