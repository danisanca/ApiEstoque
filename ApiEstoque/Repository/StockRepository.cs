using ApiEstoque.Data;
using ApiEstoque.Dtos.Stock;
using ApiEstoque.Models;
using ApiEstoque.Repository.interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class StockRepository : IStockRepository
    {
        protected readonly ApiContext _dbContext;
        private readonly IMapper _mapper;

        public StockRepository(ApiContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<StockDto> Create(StockDtoCreate product)
        {
            try
            {
                var findProduct = await _dbContext.Stock.SingleOrDefaultAsync(p => p.Barcode.Equals(product.Barcode)); 
                if (findProduct != null){
                    throw new ArgumentException("Código de barra já cadastrado.");
                }
                var validProductId = await _dbContext.Products.SingleOrDefaultAsync(p => p.Id.Equals(product.ProductId));
                if (validProductId == null)
                {
                    throw new ArgumentException($"Id do produto: {product.ProductId}, não encontrado.");
                }
                var model = _mapper.Map<StockModel>(product);
                model.Status = "Adicionado";
                model.CreateAt = DateTime.UtcNow;

                _dbContext.Stock.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<StockDto>(model);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                StockModel result = await _dbContext.Stock.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                {
                    throw new ArgumentException($"Cargo para o ID: {id} não foi encontrado no banco de dados.");
                }
                _dbContext.Stock.Remove(result);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<StockDto> Update(StockDtoUpdate product, int id)
        {
            try
            {
                StockModel result = await _dbContext.Stock.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                {
                    throw new ArgumentException($"Cargo para o ID: {id} não foi encontrado no banco de dados.");
                }
                result.Amount = product.Amount;
                result.UpdateAt = DateTime.UtcNow;
                if (product.Status != null)
                {
                    result.Status = product.Status;
                }
                _dbContext.Stock.Update(result);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<StockDto>(result);



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<StockDto>> GetAll()
        {
            try
            {
                var listStock = await _dbContext.Stock.ToListAsync();
                return _mapper.Map<List<StockDto>>(listStock);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<StockDto> GetById(int id)
        {
            try
            {
                var product = await _dbContext.Stock.FirstOrDefaultAsync(x => x.Id == id);
                return _mapper.Map<StockDto>(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<StockDto> GetProductStockByBarcode(string barcode)
        {
            try
            {
                var product = await _dbContext.Stock.FirstOrDefaultAsync(x => x.Barcode == barcode);
                return _mapper.Map<StockDto>(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
