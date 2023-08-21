namespace ApiEstoque.Models
{
    public class EmployeeOfficeModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public int OfficeId { get; set; }
        public OfficeModel Office { get; set; }
    }
}
