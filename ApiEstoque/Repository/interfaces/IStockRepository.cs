using ApiEstoque.Dtos.Product;
using ApiEstoque.Dtos.Stock;

namespace ApiEstoque.Repository.interfaces
{
    public interface IStockRepository
    {
        Task<StockDto> Create(StockDtoCreate product);
        Task<StockDto> Update(StockDtoUpdate product,int id);
        Task<bool> Delete(int id);
        Task<StockDto> GetById(int id);
        Task<List<StockDto>> GetAll();
        Task<StockDto> GetProductStockByBarcode(string barcode);
    }
}
