using ApiEstoque.Attributes;
using ApiEstoque.Dtos.User;
using ApiEstoque.Models;
using ApiEstoque.Repository.Exceptions;
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
        [Route("Create")]
        public async Task<ActionResult<UserDto>> Cadastrar([FromBody] UserAdmDtoCreate userDtoCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UserDto user = await _userRepository.CreateAdm(userDtoCreate);
                return Ok(user);
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
        [HttpPost]
        [Route("CreateFuncionario")]
        public async Task<ActionResult<UserDto>> CadastrarFuncionario([FromBody] UserPadraoDtoCreate userDtoCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UserDto user = await _userRepository.CreatePadrao(userDtoCreate);
                return Ok(user);
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
        [Route("GetAll")]
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
        [Authorize("Bearer")]
        [HttpGet]
        [Route("GetById/{id}")]
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
                    return NotFound($"Usuario para o ID: {id} não foi encontrado no banco de dados.");
                }
                else return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Put([FromBody] UserDtoUpdate user, int id)
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
                    return NotFound($"Usuario para o ID: {id} não foi encontrado no banco de dados.");
                }
                else
                {
                    var result = await _userRepository.Update(user, id);
                    return Ok(result);
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("FindEmail")]
        public async Task<ActionResult<UserDto>> FindEmail(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _userRepository.FindEmail(email);
                if (result == null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, "Email não cadastrado.");
                }
                else return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Houve uma falha na requisição");
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
                return StatusCode((int)HttpStatusCode.InternalServerError, "Houve uma falha na requisição");
            }
        }
    }
}
