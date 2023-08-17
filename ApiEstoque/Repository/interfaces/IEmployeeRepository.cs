using ApiEstoque.Dtos.Employee;
using ApiEstoque.Models;

namespace ApiEstoque.Repository.interfaces
{
    public interface IEmployeeRepository
    {
        Task<EmployeeModel> Create(EmployeeDtoCreate employee);
        Task<EmployeeModel> GetByIdUser(int idUser);
    }
}
