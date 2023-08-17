using ApiEstoque.Dtos.Office;
using ApiEstoque.Dtos.Product;
using ApiEstoque.Dtos.Stock;
using ApiEstoque.Models;
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
    public class StockController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public StockController(IStockRepository stockRepository, IProductsRepository productsRepository, IOfficeRepository officeRepository, IUserRepository userRepository, IEmployeeRepository employeeRepository)
        {
            _productsRepository = productsRepository;
            _officeRepository = officeRepository;
            _userRepository = userRepository;
            _stockRepository = stockRepository;
            _employeeRepository = employeeRepository;

        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<StockDto>> Cadastrar([FromBody] StockDtoCreate stockDtoCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                EmployeeModel employeeResult = await _employeeRepository.GetByIdUser(int.Parse(userLogged));
                OfficeDto officeUserLogged = await _officeRepository.GetById(employeeResult.OfficeId);
                if (officeUserLogged.Name == "Estoquista" || officeUserLogged.Name == "Propietario")
                {
                    StockDto productDto = await _stockRepository.Create(stockDtoCreate);
                    return Ok(productDto);
                }
                else
                {
                    return new UnauthorizedResult();

                }

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Authorize("Bearer")]
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<ActionResult<StockDto>> Atualizar([FromBody] StockDtoUpdate stockDtoUpdate, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                EmployeeModel employeeResult = await _employeeRepository.GetByIdUser(int.Parse(userLogged));
                OfficeDto officeUserLogged = await _officeRepository.GetById(employeeResult.OfficeId);
                if (officeUserLogged.Name == "Estoquista" || officeUserLogged.Name == "Propietario")
                {
                    var findUser = await _userRepository.GetById(id);
                    if (findUser == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var result = await _stockRepository.Update(stockDtoUpdate, id);
                        return Ok(result);
                    }
                }
                else
                {
                    return new UnauthorizedResult();

                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Authorize("Bearer")]
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> Deletar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                EmployeeModel employeeResult = await _employeeRepository.GetByIdUser(int.Parse(userLogged));
                OfficeDto officeUserLogged = await _officeRepository.GetById(employeeResult.OfficeId);
                if (officeUserLogged.Name == "Estoquista" || officeUserLogged.Name == "Propietario")
                {
                    var findUser = await _productsRepository.GetById(id);
                    if (findUser == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(await _stockRepository.Delete(id));
                    }
                }
                else
                {
                    return new UnauthorizedResult();

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
        public async Task<ActionResult<List<StockDto>>> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                EmployeeModel employeeResult = await _employeeRepository.GetByIdUser(int.Parse(userLogged));
                OfficeDto officeUserLogged = await _officeRepository.GetById(employeeResult.OfficeId);
                if (officeUserLogged.Name == "Estoquista" || officeUserLogged.Name == "Propietario")
                {
                    List<StockDto> products = await _stockRepository.GetAll();
                    return Ok(products);
                }
                else
                {
                    return new UnauthorizedResult();

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
        public async Task<ActionResult<StockDto>> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                EmployeeModel employeeResult = await _employeeRepository.GetByIdUser(int.Parse(userLogged));
                OfficeDto officeUserLogged = await _officeRepository.GetById(employeeResult.OfficeId);
                if (officeUserLogged.Name == "Estoquista" || officeUserLogged.Name == "Propietario")
                {
                    var result = await _stockRepository.GetById(id);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    else return Ok(result);
                }
                else
                {
                    return new UnauthorizedResult();

                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("GetByBarcode/{barcode}")]
        public async Task<ActionResult<StockDto>> GetByProductCode(string barcode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                EmployeeModel employeeResult = await _employeeRepository.GetByIdUser(int.Parse(userLogged));
                OfficeDto officeUserLogged = await _officeRepository.GetById(employeeResult.OfficeId);
                if (officeUserLogged.Name == "Estoquista" || officeUserLogged.Name == "Propietario")
                {
                    var result = await _stockRepository.GetProductStockByBarcode(barcode);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    else return Ok(result);
                }
                else
                {
                    return new UnauthorizedResult();

                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

    }
}
