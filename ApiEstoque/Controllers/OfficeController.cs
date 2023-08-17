using ApiEstoque.Dtos.Office;
using ApiEstoque.Dtos.Shop;
using ApiEstoque.Dtos.User;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {

        private readonly IOfficeRepository _officeRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private IConfiguration _configuration { get; }
        public OfficeController(IEmployeeRepository employeeRepository,IOfficeRepository officeRepository, IUserRepository userRepository, IConfiguration configuration, IShopRepository shopRepository)
        {
            _employeeRepository = employeeRepository;
            _officeRepository = officeRepository;
            _userRepository = userRepository;
            _configuration = configuration;
            _shopRepository = shopRepository;
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<OfficeDto>> Cadastrar([FromBody] OfficeDtoCreate officeDtoCreate, string? tokenAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var token = _configuration["TokenAdmin"];
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ShopDto shop = await _shopRepository.GetById(officeDtoCreate.ShopId);
                
                if (shop != null)
                {
                 OfficeDto FindOffice = await _officeRepository.GetByNameByShop(officeDtoCreate.Name,officeDtoCreate.ShopId);

                    if (tokenAdmin != null)
                    {
                        if (tokenAdmin == token)
                        {
                            
                            if (FindOffice == null)
                            {
                                OfficeDto office = await _officeRepository.Create(officeDtoCreate);
                                return Ok(office);
                            }
                            else
                            {
                                return BadRequest("Usuario já possui um cargo..");
                            }
                        }
                        else
                        {
                            return BadRequest("Token invalido.");
                        }
                    }
                    else
                    {
                        EmployeeModel employee = await _employeeRepository.GetByIdUser(int.Parse(userLogged));
                        OfficeDto officeUserLogged = await _officeRepository.GetById(employee.OfficeId);

                        if (officeUserLogged.Name == "Propietario")
                        {
                            OfficeDto office = await _officeRepository.Create(officeDtoCreate);
                            return Ok(office);
                        }
                        else
                        {
                            return BadRequest("Usuario não possuir acesso.");

                        }

                        
                    }
                }
                else
                {
                    return NotFound($"Shop id: {officeDtoCreate.ShopId} não encontrado.");
                }



            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }
        [Authorize("Bearer")]
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<OfficeDto>>> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                EmployeeModel employee = await _employeeRepository.GetByIdUser(int.Parse(userLogged));
                OfficeDto officeUserLogged = await _officeRepository.GetById(employee.OfficeId);

                if (officeUserLogged.Name == "Propietario")
                {
                    List<OfficeDto> offices = await _officeRepository.GetAllByShop(employee.ShopId);
                    return Ok(offices);
                }
                else
                {
                    return BadRequest("Usuario não possuir acesso.");
                }
                
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }
        [Authorize("Bearer")]
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult<OfficeDto>> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                EmployeeModel employee = await _employeeRepository.GetByIdUser(int.Parse(userLogged));
                OfficeDto officeUserLogged = await _officeRepository.GetById(employee.OfficeId);

                if (officeUserLogged.Name == "Propietario")
                {
                    var result = await _officeRepository.GetById(id);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    else return Ok(result);
                }
                else
                {
                    return BadRequest("Usuario não possuir acesso.");
                }

                
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [Authorize("Bearer")]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                EmployeeModel employee = await _employeeRepository.GetByIdUser(int.Parse(userLogged));
                OfficeDto officeUserLogged = await _officeRepository.GetById(employee.OfficeId);

                if (officeUserLogged.Name == "Propietario")
                {
                    var office = await _officeRepository.GetById(id);
                    if (office == null)
                    {
                        return NotFound($"Cargo com o id:{id} não encontrado.");
                    }
                    else
                    {
                        return Ok(await _officeRepository.delete(id));
                    }
                }
                else
                {
                    return BadRequest("Usuario não possuir acesso.");
                }
               

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


    }
}
