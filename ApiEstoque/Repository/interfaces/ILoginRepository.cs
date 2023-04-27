using ApiEstoque.Dtos;

namespace ApiEstoque.Repository.interfaces
{
    public interface ILoginRepository
    {
        Task<object> Login(LoginDto user);
    }
}
