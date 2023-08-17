using ApiEstoque.Dtos;
using ApiEstoque.Dtos.User;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository.interfaces;
using ApiEstoque.Security;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace ApiEstoque.Repository
{
    public class LoginRepository : ILoginRepository
    {
    
        private IUserRepository _userRepository;
        private IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private SigningConfigurations _signingConfigurations;
        private IConfiguration _configuration { get; }

        public LoginRepository(IUserRepository userRepository, IMapper mapper, SigningConfigurations signingConfigurations,
                            IConfiguration configuration,IEmployeeRepository employeeRepository)
        {

            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _signingConfigurations = signingConfigurations;
            _configuration = configuration;
        }


        public async Task<object> Login(LoginDto user)
        {
            var baseUser = new UserModel();

            if (user != null && !string.IsNullOrWhiteSpace(user.Email) && !string.IsNullOrWhiteSpace(user.Password))
            {
                baseUser = _mapper.Map<UserModel>(await _userRepository.GetByEmail(user.Email));
                

                if (baseUser != null)
                {
                    EmployeeModel employeeShop = await _employeeRepository.GetByIdUser(baseUser.Id);
                    user.SetPasswordHash();
                    if (baseUser.Password == user.Password)
                    {
                        var identity = new ClaimsIdentity(
                        new GenericIdentity(baseUser.Email),
                        new[]{
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                            new Claim(ClaimTypes.NameIdentifier,$"{baseUser.Id}"),
                        }
                    );
                        DateTime createDate = DateTime.Now;
                        DateTime expirationDate = createDate + TimeSpan.FromSeconds(Convert.ToInt32(_configuration["Seconds"]));
                        var handler = new JwtSecurityTokenHandler();
                        string token = CreateToken(identity, createDate, expirationDate, handler);
                        return SuccessObject(createDate, expirationDate, token, user, employeeShop.ShopId);
                    }
                    else
                    {
                        return new
                        {
                            authenticated = false,
                            message = "Senha Incorreta"
                        };
                    }
                }
                else
                {
                    return new
                    {
                        authenticated = false,
                        message = "Login não Encontrado"
                    };
                }



            }
            else
            {
                return null;
            }
        }
        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _configuration["Issuer"],
                Audience = _configuration["Audience"],
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });
            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, LoginDto user,int shopId)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = user.Email,
                shopId = shopId,
                message = "Usuário logado com sucesso."
            };
        }

    }
}
