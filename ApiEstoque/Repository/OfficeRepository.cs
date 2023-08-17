using ApiEstoque.Data;
using ApiEstoque.Dtos.Office;
using ApiEstoque.Dtos.User;
using ApiEstoque.Models;
using ApiEstoque.Repository.interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiEstoque.Repository
{
    public class OfficeRepository : IOfficeRepository
    {
        protected readonly ApiContext _dbContext;
        private readonly IMapper _mapper;

        public OfficeRepository(ApiContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OfficeDto> Create(OfficeDtoCreate office)
        {
            try
            {
                OfficeModel findOffice = _mapper.Map<OfficeModel>(await GetByNameByShop(office.Name,office.ShopId));
                if (findOffice == null)
                {
                    var model = _mapper.Map<OfficeModel>(office);
                    model.CreateAt = DateTime.UtcNow;
                    await _dbContext.Offices.AddAsync(model);
                    await _dbContext.SaveChangesAsync();
                    return _mapper.Map<OfficeDto>(model);
                }
                else
                {
                    if (office.Name.ToLower() != findOffice.Name.ToLower())
                    {
                        var model = _mapper.Map<OfficeModel>(office);
                        model.CreateAt = DateTime.UtcNow;
                        await _dbContext.Offices.AddAsync(model);
                        await _dbContext.SaveChangesAsync();
                        return _mapper.Map<OfficeDto>(model);
                    }
                    throw new ArgumentException($"Ja existe um office com o nome: {office.Name} cadastrado o usuario: {office.ShopId}.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> delete(int id)
        {
            try
            {
                OfficeModel office = await _dbContext.Offices.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (office == null)
                {
                    throw new ArgumentException($"Cargo para o ID: {id} não foi encontrado no banco de dados.");
                }
                _dbContext.Offices.Remove(office);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OfficeDto>> GetAllByShop(int idShop)
        {
            try
            {
                var listOffice = await _dbContext.Offices.Where(x => x.ShopId == idShop).ToListAsync();
                return _mapper.Map<List<OfficeDto>>(listOffice);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OfficeDto> GetById(int id)
        {
            try
            {
                var office = await _dbContext.Offices.FirstOrDefaultAsync(x => x.Id == id);
                return _mapper.Map<OfficeDto>(office);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OfficeDto> GetByNameByShop(string name,int idShop)
        {
            try
            {
                var office = await _dbContext.Offices.FirstOrDefaultAsync(x => x.Name == name && x.ShopId == idShop);
                return _mapper.Map<OfficeDto>(office);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
