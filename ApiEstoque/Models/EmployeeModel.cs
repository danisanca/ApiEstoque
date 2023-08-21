namespace ApiEstoque.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public int ShopId { get; set; }
        public ShopModel Shop { get; set; }
        private DateTime _createAt;
        public DateTime CreateAt
        {
            get { return _createAt; }
            set
            {
                _createAt = value == null ? DateTime.UtcNow : value;
            }
        }
    }
}
