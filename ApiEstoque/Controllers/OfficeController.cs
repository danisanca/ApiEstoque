using ApiEstoque.Dtos.Office;
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
    public class OfficeController : ControllerBase
    {

        private readonly IOfficeRepository _officeRepository;
        private readonly IUserRepository _userRepository;
        private IConfiguration _configuration { get; }
        public OfficeController(IOfficeRepository officeRepository, IUserRepository userRepository, IConfiguration configuration)
        {
            _officeRepository = officeRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [Authorize("Bearer")]
        [HttpPost]
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
                UserDto user = await _userRepository.GetById(officeDtoCreate.UserId);
                
                if (user != null)
                {
                 OfficeDto FindOffice = await _officeRepository.GetByUserId(officeDtoCreate.UserId);

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
                        OfficeDto officeUserLogged = await _officeRepository.GetByUserId(int.Parse(userLogged));

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
                    return NotFound($"Usuario id: {officeDtoCreate.UserId} não encontrado.");
                }



            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }
        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult<List<OfficeDto>>> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                OfficeDto officeUserLogged = await _officeRepository.GetByUserId(int.Parse(userLogged));

                if (officeUserLogged.Name == "Propietario")
                {
                    List<OfficeDto> offices = await _officeRepository.GetAll();
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
        [Route("{id}")]
        public async Task<ActionResult<OfficeDto>> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                OfficeDto officeUserLogged = await _officeRepository.GetByUserId(int.Parse(userLogged));

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
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userLogged = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                OfficeDto officeUserLogged = await _officeRepository.GetByUserId(int.Parse(userLogged));

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
