using ApiEstoque.Dtos.Employee;
using ApiEstoque.Dtos.Office;
using ApiEstoque.Dtos.Product;
using ApiEstoque.Dtos.Shop;
using ApiEstoque.Dtos.Stock;
using ApiEstoque.Dtos.Transaction_History;
using ApiEstoque.Dtos.User;
using ApiEstoque.Models;
using AutoMapper;

namespace ApiEstoque.Mapping
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile() {

            #region User
            CreateMap<UserModel, UserDto>()
                    .ReverseMap();
            CreateMap<UserModel, UserAdmDtoCreate>()
                    .ReverseMap();
            CreateMap<UserModel, UserPadraoDtoCreate>()
                   .ReverseMap();
            CreateMap<UserModel, UserDtoUpdate>()
                    .ReverseMap();
            #endregion

            #region Office
            CreateMap<OfficeModel, OfficeDto>()
                    .ReverseMap();
            CreateMap<OfficeModel, OfficeDtoCreate>()
                    .ReverseMap();
            #endregion

            #region Product
            CreateMap<ProductModel, ProductDto>()
                    .ReverseMap();
            CreateMap<ProductModel, ProductDtoCreate>()
                    .ReverseMap();
            CreateMap<ProductModel, ProductDtoUpdate>()
                    .ReverseMap();
            #endregion

            #region Stock
            CreateMap<StockModel, StockDto>()
                    .ReverseMap();
            CreateMap<StockModel, StockDtoCreate>()
                    .ReverseMap();
            CreateMap<StockModel, StockDtoUpdate>()
                    .ReverseMap();
            #endregion

            #region TransactionHistory
            CreateMap<TransactionHistoryModel, TransactionHistoryDto>()
                    .ReverseMap();
            CreateMap<TransactionHistoryModel, TransactionHistoryDtoCreate>()
                    .ReverseMap();
            #endregion
            #region TShop
            CreateMap<ShopModel, ShopDto>()
                    .ReverseMap();
            CreateMap<ShopModel, ShopDtoCreate>()
                    .ReverseMap();
            #endregion

            #region TEmployee
            CreateMap<EmployeeModel, EmployeeDtoCreate>()
                    .ReverseMap();
           
            #endregion

        }

    }
}
