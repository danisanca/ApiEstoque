using ApiEstoque.Dtos.Product;

namespace ApiEstoque.Repository.interfaces
{
    public interface IProductsRepository
    {
        Task<ProductDto> Create(ProductDtoCreate product);
        Task<ProductDto> Update(ProductDtoUpdate product, int id);
        Task<bool> Delete(int id);
        Task<ProductDto> GetById(int id);
        Task<List<ProductDto>> GetAll();
        Task<ProductDto> GetProductCode(string productCode);
        Task<bool> VerifyProductExist(string? name,string? productCode);
    }
}
