using ApiEstoque.Dtos.Office;

namespace ApiEstoque.Repository.interfaces
{
    public interface IOfficeRepository
    {
        Task<List<OfficeDto>> GetAll();
        Task<OfficeDto> GetById(int id);
        Task<OfficeDto> GetByUserId(int id);
        Task<OfficeDto> GetByName(string name);
        Task<OfficeDto> Create(OfficeDtoCreate office);
        Task<bool> delete(int id);
    }
}
