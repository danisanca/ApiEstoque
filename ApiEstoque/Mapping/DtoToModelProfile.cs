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
            CreateMap<UserModel, UserDtoCreate>()
                    .ReverseMap();
            CreateMap<UserModel, UserDtoUpdate>()
                    .ReverseMap();
            #endregion

        }

    }
}
