using ApiEstoque.Dtos.Product;
using ApiEstoque.Dtos.Transaction_History;

namespace ApiEstoque.Repository.interfaces
{
    public interface ITransactionHistoryRepository
    {
        Task<TransactionHistoryDto> Create(TransactionHistoryDtoCreate data);
        Task<TransactionHistoryDto> GetById(int id);
        Task<List<TransactionHistoryDto>> GetAll();
    }
}
