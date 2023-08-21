using ApiEstoque.Helpers;

namespace ApiEstoque.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }

        private DateTime _createAt;
        public DateTime CreateAt 
        { 
            get { return _createAt; } 
            set {
                _createAt = value == null ? DateTime.UtcNow : value; 
            } 
        }
        public DateTime? UpdateAt { get; set; }

        public void SetPasswordHash()
        {
            Password = Password.CreateHash();
        }
    }
}
