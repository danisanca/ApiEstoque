using ApiEstoque.Dtos.User;
using ApiEstoque.Repository.Exceptions;
using ApiEstoque.Repository;
using ApiEstoque.Repository.interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApiEstoque.Models;
using ApiEstoque.Dtos.Employee;
using Microsoft.AspNetCore.Authorization;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<EmployeeModel>> Cadastrar([FromBody] EmployeeDtoCreate employeeDtoCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EmployeeModel employee = await _employeeRepository.Create(employeeDtoCreate);
                return Ok(employee);
            }
            catch (FailureRequestException e)
            {
                if (e.StatusCode == 404)
                {
                    return StatusCode((int)HttpStatusCode.Conflict, e.Message);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }
        [Authorize("Bearer")]
        [HttpGet]
        [Route("GetByIdUser/{idUser}")]
        public async Task<ActionResult<EmployeeModel>> GetById(int idUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _employeeRepository.GetByIdUser(idUser);
                if (result == null)
                {
                    return NotFound($"Usuario para o ID: {idUser} não foi encontrado no banco de dados.");
                }
                else return Ok(result);
            }
            catch (FailureRequestException e)
            {
                if (e.StatusCode == 404)
                {
                    return StatusCode((int)HttpStatusCode.Conflict, e.Message);
                }
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
