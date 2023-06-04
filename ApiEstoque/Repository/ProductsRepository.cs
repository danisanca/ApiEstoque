using ApiEstoque.Data;
using ApiEstoque.Dtos.Office;
using ApiEstoque.Dtos.Product;
using ApiEstoque.Models;
using ApiEstoque.Repository.interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiEstoque.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        protected readonly ApiContext _dbContext;
        private readonly IMapper _mapper;

        public ProductsRepository(ApiContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductDto> Create(ProductDtoCreate product)
        {
            try
            {
                var findProductExist = await VerifyProductExist(product.Name,product.ProductCode);
                if (findProductExist == true)
                {
                    throw new ArgumentException("Produto já cadastrado.");
                }

                if (Regex.IsMatch(product.Price.ToString(), @"^\d+(\.\d+)?$"))
                {
                    throw new ArgumentException("Preço invalido.");
                }
                var findShop = await _dbContext.Shop.SingleOrDefaultAsync(p => p.Id.Equals(product.ShopId));
                if (findShop == null)
                {
                    throw new ArgumentException($"ShopId: {product.ShopId} não foi encontrada no banco de dados.");
                }
                var model = _mapper.Map<ProductModel>(product);
                model.Status = "Criado";
                model.CreateAt = DateTime.UtcNow;

                _dbContext.Products.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<ProductDto>(model);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductDto> Update(ProductDtoUpdate product, int id)
        {
            try
            {
                ProductModel productResult = await _dbContext.Products.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (productResult == null)
                {
                    throw new ArgumentException($"Usuario para o ID: {id} não foi encontrado no banco de dados.");
                }
                if (Regex.IsMatch(product.Price.ToString(), @"^\d+(\.\d+)?$"))
                {
                    throw new ArgumentException("Preço invalido.");
                }
                productResult.Name = product.Name;
                productResult.Description = product.Description;
                productResult.Price = product.Price;
                productResult.UnitOfMeasure = product.UnitOfMeasure;
                productResult.Status = "Atualizado";
                productResult.UpdateAt = DateTime.UtcNow;

                _dbContext.Products.Entry(productResult).CurrentValues.SetValues(productResult);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<ProductDto>(productResult);


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
                ProductModel result = await _dbContext.Products.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                {
                    throw new ArgumentException($"Cargo para o ID: {id} não foi encontrado no banco de dados.");
                }
                _dbContext.Products.Remove(result);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProductDto>> GetAll()
        {
            try
            {
                var listProduct = await _dbContext.Products.ToListAsync();
                return _mapper.Map<List<ProductDto>>(listProduct);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductDto> GetById(int id)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> VerifyProductExist(string? name, string? productCode)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Name == name);
                if (product != null)
                {
                    if (product.ProductCode == productCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductDto> GetProductCode(string productCode)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductCode == productCode);
                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
