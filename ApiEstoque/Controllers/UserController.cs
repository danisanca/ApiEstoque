using ApiEstoque.Attributes;
using ApiEstoque.Dtos.User;
using ApiEstoque.Models;
using ApiEstoque.Repository.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace ApiEstoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPost]
        public async Task<ActionResult<UserDto>> Cadastrar([FromBody] UserDtoCreate userDtoCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UserDto user = await _userRepository.Create(userDtoCreate);
                return Ok(user);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
            
        }

        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<UserDto> users = await _userRepository.GetAll();
                return Ok(users);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
           
        }
        //[ClaimsAuthorize("Cargo", "Estoquistas")]
        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _userRepository.GetById(id);
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

        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] UserDtoUpdate user,int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var findUser = await _userRepository.GetById(id);
                if (findUser == null)
                {
                    return NotFound();
                }
                else
                {
                    var result = await _userRepository.Update(user,id);
                    return Ok(result);
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var findUser = await _userRepository.GetById(id);
                if (findUser == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(await _userRepository.Delete(id));
                }
                    
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
