﻿namespace ApiEstoque.Models
{
    public class OfficeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
