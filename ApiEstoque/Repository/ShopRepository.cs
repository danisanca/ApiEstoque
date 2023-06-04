using ApiEstoque.Data;
using ApiEstoque.Dtos.Product;
using ApiEstoque.Dtos.Shop;
using ApiEstoque.Models;
using ApiEstoque.Repository.interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class ShopRepository : IShopRepository
    {
        protected readonly ApiContext _dbContext;
        private readonly IMapper _mapper;

        public ShopRepository(ApiContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ShopDto> Create(ShopDtoCreate shop)
        {
            try
            {
                UserModel userResult = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id.Equals(shop.UserId));
                if (userResult == null)
                {
                    throw new ArgumentException($"Usuario para o ID: {shop.UserId} não foi encontrado no banco de dados.");
                }

                var model = _mapper.Map<ShopModel>(shop);
                model.Status = "Criado";
                model.CreateAt = DateTime.UtcNow;
                _dbContext.Shop.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<ShopDto>(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ShopDto>> GetAll()
        {
            try
            {
                var listShop = await _dbContext.Shop.ToListAsync();
                return _mapper.Map<List<ShopDto>>(listShop);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ShopDto> GetById(int id)
        {
            try
            {
                var shop = await _dbContext.Shop.FirstOrDefaultAsync(x => x.Id == id);
                return _mapper.Map<ShopDto>(shop);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
