using ApiEstoque.Dtos.Office;
using ApiEstoque.Dtos.Product;
using ApiEstoque.Dtos.User;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IUserRepository _userRepository;

        public ProductController(IProductsRepository productsRepository, IOfficeRepository officeRepository, IUserRepository userRepository)
        {
            _productsRepository = productsRepository;
            _officeRepository = officeRepository;
            _userRepository = userRepository;
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<ProductDto>> Cadastrar([FromBody] ProductDtoCreate productDtoCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                OfficeDto officeUserLogged = await _officeRepository.GetByUserId(int.Parse(userLogged));
                if (officeUserLogged.Name == "Estoquista"|| officeUserLogged.Name == "Propietario")
                {
                    ProductDto productDto = await _productsRepository.Create(productDtoCreate);
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
        public async Task<ActionResult<ProductDto>> Atualizar([FromBody] ProductDtoUpdate productDtoUpdate,int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                OfficeDto officeUserLogged = await _officeRepository.GetByUserId(int.Parse(userLogged));
                if (officeUserLogged.Name == "Estoquista" || officeUserLogged.Name == "Propietario")
                {
                    var findUser = await _userRepository.GetById(id);
                    if (findUser == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var result = await _productsRepository.Update(productDtoUpdate, id);
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
                OfficeDto officeUserLogged = await _officeRepository.GetByUserId(int.Parse(userLogged));
                if (officeUserLogged.Name == "Estoquista" || officeUserLogged.Name == "Propietario")
                {
                    var findUser = await _productsRepository.GetById(id);
                    if (findUser == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Ok(await _productsRepository.Delete(id));
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
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                OfficeDto officeUserLogged = await _officeRepository.GetByUserId(int.Parse(userLogged));
                if (officeUserLogged.Name == "Estoquista" || officeUserLogged.Name == "Propietario")
                { 
                    List<ProductDto> products  = await _productsRepository.GetAll();
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
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                OfficeDto officeUserLogged = await _officeRepository.GetByUserId(int.Parse(userLogged));
                if (officeUserLogged.Name == "Estoquista" || officeUserLogged.Name == "Propietario")
                {
                    var result = await _productsRepository.GetById(id);
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
        [Route("GetByProductCode/{productCode}")]
        public async Task<ActionResult<ProductDto>> GetByProductCode(string productCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                OfficeDto officeUserLogged = await _officeRepository.GetByUserId(int.Parse(userLogged));
                if (officeUserLogged.Name == "Estoquista" || officeUserLogged.Name == "Propietario")
                {
                    var result = await _productsRepository.GetProductCode(productCode);
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
