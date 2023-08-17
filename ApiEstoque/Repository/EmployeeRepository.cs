using ApiEstoque.Data;
using ApiEstoque.Dtos.Employee;
using ApiEstoque.Models;
using ApiEstoque.Repository.Exceptions;
using ApiEstoque.Repository.interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected readonly ApiContext _dbContext;
        private readonly IMapper _mapper;
        public EmployeeRepository(ApiContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<EmployeeModel> Create(EmployeeDtoCreate employee)
        {
            try
            {
                UserModel userResult = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id.Equals(employee.UserId));
                if (userResult == null)
                {
                    throw new FailureRequestException(404,"Usuario não encontrado.");
                }
                ShopModel shopResult = await _dbContext.Shop.SingleOrDefaultAsync(p => p.Id.Equals(employee.ShopId));
                if (shopResult == null)
                {
                    throw new FailureRequestException(404, "Shop não encontrado.");
                }
                var model = _mapper.Map<EmployeeModel>(employee);
                model.Status = "Criado";
                await _dbContext.Employee.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return model;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeModel> GetByIdUser(int idUser)
        {
            try
            {
                EmployeeModel employeeResult = await _dbContext.Employee.SingleOrDefaultAsync(p => p.UserId.Equals(idUser));
                if (employeeResult == null)
                {
                    throw new FailureRequestException(404, "Usuario não encontrado.");
                }
                return employeeResult;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
