
using ApiEstoque.Dtos.Shop;

namespace ApiEstoque.Repository.interfaces
{
    public interface IShopRepository
    {
        Task<ShopDto> Create(ShopDtoCreate shop);
        Task<ShopDto> GetById(int id);
        Task<ShopDto> GetByName(string name);
        Task<List<ShopDto>> GetAll();
    }
}
