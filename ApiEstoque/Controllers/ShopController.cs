using ApiEstoque.Dtos.Office;
using ApiEstoque.Dtos.Product;
using ApiEstoque.Dtos.Shop;
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
    public class ShopController : ControllerBase
    {
        private readonly IShopRepository _shopRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IUserRepository _userRepository;

        public ShopController(IShopRepository shopRepository, IOfficeRepository officeRepository, IUserRepository userRepository)
        {
            _shopRepository = shopRepository;
            _officeRepository = officeRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<ShopDto>> Cadastrar([FromBody] ShopDtoCreate shopDtoCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ShopDto shop = await _shopRepository.Create(shopDtoCreate);
                return Ok(shop);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<ShopDto>>> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<ShopDto> shops = await _shopRepository.GetAll();
                return Ok(shops);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }
        [Authorize("Bearer")]
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ActionResult<ShopDto>> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _shopRepository.GetById(id);
                if (result == null)
                {
                    return NotFound();
                }
                else return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


    }
}
