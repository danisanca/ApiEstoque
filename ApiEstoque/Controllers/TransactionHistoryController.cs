using ApiEstoque.Dtos.Office;
using ApiEstoque.Dtos.Product;
using ApiEstoque.Dtos.Transaction_History;
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
    public class TransactionHistoryController : ControllerBase
    {
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;

        public TransactionHistoryController(ITransactionHistoryRepository transactionHistoryRepository)
        {
            _transactionHistoryRepository = transactionHistoryRepository;

        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<TransactionHistoryDto>> Cadastrar([FromBody] TransactionHistoryDtoCreate transactionHistoryDtoCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TransactionHistoryDto transaction = await _transactionHistoryRepository.Create(transactionHistoryDtoCreate);
                return Ok(transaction);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<TransactionHistoryDto>>> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               
                    List<TransactionHistoryDto> transactions = await _transactionHistoryRepository.GetAll();
                    return Ok(transactions);
              
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
              
                    var transaction = await _transactionHistoryRepository.GetById(id);
                    if (transaction == null)
                    {
                        return NotFound();
                    }
                    else return Ok(transaction);
                
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

    }
}
