using ApiEstoque.Dtos.Office;

namespace ApiEstoque.Repository.interfaces
{
    public interface IOfficeRepository
    {
        Task<List<OfficeDto>> GetAllByShop(int idShop);
        Task<OfficeDto> GetById(int id);
        Task<OfficeDto> GetByNameByShop(string name, int idShop);
        Task<OfficeDto> Create(OfficeDtoCreate office);
        Task<bool> delete(int id);
    }
}
