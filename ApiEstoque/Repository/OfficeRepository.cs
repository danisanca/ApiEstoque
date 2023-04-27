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
                OfficeModel findOffice = _mapper.Map<OfficeModel>(await GetByUserId(office.UserId));
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
                    throw new Exception($"Ja existe um office com o nome: {office.Name} cadastrado o usuario: {office.UserId}.");
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
                OfficeModel office = _mapper.Map<OfficeModel>(await GetById(id));
                if (office == null)
                {
                    throw new Exception($"Cargo para o ID: {id} não foi encontrado no banco de dados.");
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

        public async Task<List<OfficeDto>> GetAll()
        {
            try
            {
                var listOffice = await _dbContext.Offices.ToListAsync();
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

        public async Task<OfficeDto> GetByName(string name)
        {
            try
            {
                var office = await _dbContext.Offices.FirstOrDefaultAsync(x => x.Name == name);
                return _mapper.Map<OfficeDto>(office);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OfficeDto> GetByUserId(int id)
        {
            try
            {
                var office = await _dbContext.Offices.FirstOrDefaultAsync(x => x.UserId == id);
                return _mapper.Map<OfficeDto>(office);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
