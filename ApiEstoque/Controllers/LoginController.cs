using ApiEstoque.Dtos;
using ApiEstoque.Repository.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _loginRepository.Login(user);
                
                if (result != null)
                {
                    dynamic jsonResult = JsonConvert.DeserializeObject(System.Text.Json.JsonSerializer.Serialize(result));
                    if (jsonResult.authenticated == false)
                    {
                        return BadRequest(result);
                    }
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
