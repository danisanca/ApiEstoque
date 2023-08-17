using ApiEstoque.Dtos.User;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.interfaces
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task<UserModel> GetByEmail(string email);
        Task<UserDto> FindEmail(string email);
        Task<UserDto> CreateAdm(UserAdmDtoCreate user);
        Task<UserDto> CreatePadrao(UserPadraoDtoCreate user);
        Task<UserDto> Update(UserDtoUpdate user, int id);
        Task<bool> Delete(int id);
    }
}
