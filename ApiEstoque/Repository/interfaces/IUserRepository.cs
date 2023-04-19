using ApiEstoque.Dtos.User;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAll();
        Task<UserModel> GetById(int id);
        Task<UserModel> Create(UserDtoCreate user);
        Task<UserModel> Update(UserDtoUpdate user, int id);
        Task<bool> Delete(int id);
    }
}
