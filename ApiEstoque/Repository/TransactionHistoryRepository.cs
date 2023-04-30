using ApiEstoque.Data;
using ApiEstoque.Dtos.Transaction_History;
using ApiEstoque.Models;
using ApiEstoque.Repository.interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {
        protected readonly ApiContext _dbContext;
        private readonly IMapper _mapper;

        public TransactionHistoryRepository(ApiContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<TransactionHistoryDto> Create(TransactionHistoryDtoCreate data)
        {
            try
            {
                var findUser = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id.Equals(data.UserId));
                if(findUser == null)
                {
                    throw new ArgumentException($"UsuarioId: {data.UserId} não foi encontrada no banco de dados.");
                }
                var findProduct = await _dbContext.Products.SingleOrDefaultAsync(p => p.Id.Equals(data.ProductId));
                if (findProduct == null)
                {
                    throw new ArgumentException($"ProdutoId: {data.ProductId} não foi encontrada no banco de dados.");
                }
                var model = _mapper.Map<TransactionHistoryModel>(data);
                model.CreateAt = DateTime.UtcNow;
                await _dbContext.TransactionsHistory.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<TransactionHistoryDto>(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TransactionHistoryDto>> GetAll()
        {
            try
            {
                var listTransaction = await _dbContext.TransactionsHistory.ToListAsync();
                return _mapper.Map<List<TransactionHistoryDto>>(listTransaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TransactionHistoryDto> GetById(int id)
        {
            try
            {
                var transaction = await _dbContext.TransactionsHistory.FirstOrDefaultAsync(x => x.Id == id);
                return _mapper.Map<TransactionHistoryDto>(transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
